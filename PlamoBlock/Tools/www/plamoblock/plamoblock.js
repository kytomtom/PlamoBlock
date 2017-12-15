$(function() {
	var scene, renderer;
	var camera, cameraA, cameraB;
	
	var BlockSize = 2;
	var PlateSize;
	var OffsetY;
	var BlockColor;

	var ModelName;
	// EdgeColor edge … none=境界線なし / black=黒 / gray=灰色 / egde=BlockColor.edgeの値
	var EdgeColor;
	
	var SceneObj = [];
	var PartsName = [];

	var controls;

	//パラメーター
	var arg = new Object;
	var pair = location.search.substring(1).split('&');
	for(var i = 0; pair[i]; i++) {
		var kv = pair[i].split('=');
		arg[kv[0]] = kv[1];
	};
		
	init();
	animate();

	function init() {
		if (arg.model) {
			ModelName = arg.model;
		} else {
			ModelName = 'YaoRyoka';
		};
		EdgeColor = $('#EdgeColor').val();
		
		scene = new THREE.Scene();
		
		var width  = 500;
		var height = 600;
		var fov    = 60;
		var aspect = width / height;
		var scale = 0.1;
		var near   = 1;
		var far    = 1000;
		// 平行投影
		cameraA = new THREE.OrthographicCamera(-width/2*scale, width/2*scale, height/2*scale, -height/2*scale, near, far);
		cameraA.position.set(25, 10, 50);
		// 透視投影
		cameraB = new THREE.PerspectiveCamera(fov, aspect, near, far);
		cameraB.position.set(25, 10, 50);
		
		var light;
		// 手前・右上から
		light = new THREE.DirectionalLight(0xffffff);
		light.position.set(1, 1, 1).normalize();
		scene.add(light);
		// 奥・左上から
		light = new THREE.DirectionalLight(0xffffff, 0.1);
		light.position.set(-1, 1, -1).normalize();
		scene.add(light);
		// 全体
		light = new THREE.AmbientLight(0x888888);
		scene.add(light);

		/* 補助線
		var axis = new THREE.AxisHelper(1000);
		axis.position.set(0,0,0);
		scene.add(axis);
		var grid = new THREE.GridHelper(50, 2); // size, step
		scene.add(grid);
		*/

		$.ajaxSetup({cache: false});

		// ブロックの色の読み込み
		$.ajaxSetup({async: false});
		// $.getJSON('./BlockClolor.json', function(obj){BlockColor = obj;});
		$.getJSON('./getmodeldata.php?id=BlockColor', function(obj){BlockColor = obj;});
		$.ajaxSetup({async: true});

		SetModelData();

		renderer = new THREE.WebGLRenderer({antialias: true});
		renderer.setSize(width, height);
		renderer.setClearColor(0xDDDDDD, 1);

		document.getElementById('PlamoBlock').appendChild(renderer.domElement);
		
		setCamera();
	};

	function setCamera() {
		if (!camera) {
				camera = cameraA;
		} else {
			if (camera == cameraA) {
				camera = cameraB;
			} else if (camera == cameraB) {
				camera = cameraA;
			};
		};

		if (controls) controls.dispose();
		controls = new THREE.OrbitControls(camera, renderer.domElement);
		controls.update();
	}
	
	function animate() {
		requestAnimationFrame(animate);
		
		controls.update();
		renderer.render(scene, camera);
	};

	/* キャラデータ配置 */
	function SetModelData() {
		var ModelData;
		var Block;
		var LenParts, LenLayer, LenBlock;
		var l, MaxLayer;
		var BlockCount = {};
		var BlockType = ['1x1', '1x2', '1x3', '1x4', '1x6', '1x8', '2x2', '2x3', '2x4', '2x6', '2x8'];
		var buf;

		// キャラクターデータの読み込み
		$.ajaxSetup({async: false});
		// $.getJSON('./modeldata/' + ModelName + '.json', function(obj){ModelData = obj;});
		$.getJSON('./getmodeldata.php?id=' + ModelName, function(obj){ModelData = obj;});
		$.ajaxSetup({async: true});
		
		var obj = ModelData.Chara;

		// 最大の高さ取得
		MaxLayer = 1;
		LenParts = obj.Block.length;
		for (var i = 0; i < LenParts; i++) {
			l = Number(obj.Block[i].BottomPos);
			LenLayer = obj.Block[i].Layer.length;
			for (var j = 0; j < LenLayer; j++) {
				MaxLayer = Math.max(l, MaxLayer);
				l += 1;
			};
		};

		// オフセット設定
		OffsetY = MaxLayer * 0.4 * -1;

		// プレート設置
		CheckColorExists(obj.Plate[2]);
			
		PlateSize = [Number(obj.Plate[0]), Number(obj.Plate[1])];
		
		var Plate = new MakeBlock(PlateSize[0], PlateSize[1], obj.Plate[2]);
		Plate.position.y = BlockSize * (-1 + OffsetY);
		scene.add(Plate);
		SceneObj.push({object : Plate, type : 'Panel'});	
		var Plate = new MakeBlock(PlateSize[0] + 2, PlateSize[1] + 2, obj.Plate[2]);
		Plate.position.y = BlockSize * (-1 + OffsetY - 1);
		scene.add(Plate);
		SceneObj.push({object : Plate, type : 'Panel'});	
		
		// パーツごとに処理
		for (var i = 0; i < LenParts; i++) {
			PartsName[i] = obj.Block[i].Name;
			
			// パーツの最底面の位置
			l = Number(obj.Block[i].BottomPos);
			
			// 層ごとに処理
			LenLayer = obj.Block[i].Layer.length;
			for (var j = 0; j < LenLayer; j++) {
				MaxLayer = Math.max(l, MaxLayer);
			
				// ブロックごとに処理
				LenBlock = obj.Block[i].Layer[j].length;
				for (var k = 0; k < LenBlock; k++) {
					Block = obj.Block[i].Layer[j][k];
					SetBlock(i, l, Number(Block.x), Number(Block.y), Number(Block.w), Number(Block.d), Number(Block.r), Block.c);

					// ブロックをカウント
					buf = Block.w + 'x' + Block.d;
					if (!BlockCount[Block.c]) {
						BlockCount[Block.c] = {};
					};
					if (!BlockCount[Block.c][buf]) {
						BlockCount[Block.c][buf] = 0;
					};
					BlockCount[Block.c][buf] += 1;
				};
				
				l += 1;
			};
		};
		
		// モデル名表示
		var doc = document.getElementById('ModelName');
		while (doc.childNodes.length > 0) {
			doc.removeChild(doc.firstChild);
		};
		if (obj.Twitter) {
 			doc.appendChild(document.createTextNode(obj.DisplayName + '('));
 			
			var element = document.createElement('a'); 
			element.href = 'https://twitter.com/' + obj.Twitter; 
			element.innerHTML = '@' + obj.Twitter; 
 			doc.appendChild(element);

 			doc.appendChild(document.createTextNode(')だよー！'));
		} else {
 			doc.appendChild(document.createTextNode(obj.DisplayName + 'だよー！'));
		};
		
		// コピーライト表示
		var doc = document.getElementById('Copyright');
		while (doc.childNodes.length > 0) {
			doc.removeChild(doc.firstChild);
		};
		if (obj.Copyright) {
 			doc.appendChild(document.createTextNode('・当サイトは下記著作権者の画像を利用しています。該当画像の転載・配付等は禁止します。'));
 			doc.appendChild(document.createElement('br'));

			var element = document.createElement('span'); 
			element.style.marginLeft = '8px'; 
			element.innerHTML = obj.Copyright; 
 			doc.appendChild(element);
		};
		
		/* 入力フォームの設定変更 */
		// 表示レイヤー
		$('#LayerNumButtom').attr('max', MaxLayer);
		$('#LayerNumButtom').val(1);
		$('#LayerNumTop').attr('max', MaxLayer);
		$('#LayerNumTop').val(MaxLayer);
		
		// 表示パーツ
		var doc = document.getElementById('ViewParts');
		while (doc.childNodes.length > 0) {
			doc.removeChild(doc.firstChild);
		};
		for (var i in PartsName) {
			var element = document.createElement('input'); 
			element.type = 'checkbox';
			element.id = 'ViewPartsSelect';
			element.name = 'ViewPartsSelect';
			element.value = PartsName[i]; 
			element.innerHTML = PartsName[i]; 
			element.checked = 'checked';
			doc.appendChild(element);

 			doc.appendChild(document.createTextNode(PartsName[i]));
 			doc.appendChild(document.createElement('br'));
		};
		
		// 使用ブロック
		var doc = document.getElementById('UseBlock');
		while (doc.childNodes.length > 0) {
			doc.removeChild(doc.firstChild);
		};
		var element = document.createElement('table'); 
		element.id = 'ViewPartsSelect';
		buf = '<table border="1">';
		buf += '<tr><th colspan="2">使用ブロック</th>'
		for (var i in BlockType) {
			buf += '<th>' + BlockType[i] + '</th>'
		};
		buf += '</tr>'
		for (var color in BlockCount) {
			buf += '<tr><td align="right">' + BlockColor[color].kana + '</td>';
			buf += '<td bgcolor="' + BlockColor[color].base + '" width="30px"></td>';
			for (var i in BlockType) {
				buf += '<td align="center">'
				if (BlockCount[color][BlockType[i]]) {
					buf += BlockCount[color][BlockType[i]]
				};
				buf += '</td>'
			};
			buf += '</tr>'
		};
		buf += '</table>';
		doc.innerHTML = buf;
	};

	// 指定座標にブロックを作成
	//  x…横座標（1→)
	//  z…縦座標（1↓)
	//  l…高さ座標（1が底面)
	//  w…ブロックの横幅
	//  d…ブロックの縦幅
	//  r…回転（0:0 / 1:90 / 2:180 / 3: 270）
	//  c1…ブロックの面の色
	//  c2…ブロックの境界線の色
	//  op…不透明度
	function SetBlock(PartsNo, l, x, y, w, d, r, c) {
		CheckColorExists(c);

		var w1, d1;

		// 回転処理
		if (r % 2 == 0) {
			w1 = w;
			d1 = d;
		} else {
			w1 = d;
			d1 = w;
		};
		
		var Block = MakeBlock(w1, d1, c);
		var pos = BlockPos(l - 1, x - 1, y - 1, w1, d1);
		Block.position.set(pos[0], pos[1], pos[2]);

		scene.add(Block);
		SceneObj.push({object : Block, type : 'Block', layer : l, parts : PartsName[PartsNo]});
	};

	// 座標計算
	function BlockPos(l, x, y, w, d) {
		var PosX, PosY, PosL, RY;
		
		// 起点計算
		PosX = (BlockSize * PlateSize[0] / -2) + BlockSize * (x + 0.5);
		PosY = (BlockSize * PlateSize[1] / -2) + BlockSize * (y + 0.5);
		PosL = BlockSize * (l + OffsetY);
		
		// ブロックサイズ分ずらす
		PosX += BlockSize * (w / 2 - 0.5);
		PosY += BlockSize * (d / 2 - 0.5);
		
		return [PosX, PosL, PosY];
	};

	// ブロック作成
	function MakeBlock(w, d, c) {
		var Block = new THREE.Group();
		var Material, Material_Edge;
		var bs, eg, op
		
		bs = BlockColor[c].base;
		op = BlockColor[c].opacity;

		// エッジカラー設定
		eg = BlockColor[c].edge;
		switch (EdgeColor) {
			case 'black':
				if (bs != '#000000') {
					eg = '#000000';
				};
				break;
			case 'gray':
				if (bs != '#888888') {
					eg = '#888888';
				};
				break;
		};
		
		// ブロックの色
		if (op == 1) {
			Material = new THREE.MeshPhongMaterial({color: bs});
		} else {
			Material = new THREE.MeshPhongMaterial({color: bs, opacity: op, transparent: true});
		};
		Material_Edge = new THREE.LineBasicMaterial({color: eg, linewidth: 2});
		
		// ベース作成
		var Geometry_Base = new THREE.BoxGeometry(BlockSize * w, BlockSize, BlockSize * d);
		var Geometry_Base_Edge = new THREE.EdgesGeometry(Geometry_Base);
		var Mesh_Base = new THREE.Mesh(Geometry_Base, Material);
		Mesh_Base.position.set(0, 0, 0)
		Block.add(Mesh_Base);
		
		if (EdgeColor != 'none') {
		    var Edge_Base = new THREE.LineSegments(Geometry_Base_Edge, Material_Edge);
			Edge_Base.position.set(0, 0, 0);
			Block.add(Edge_Base);
		};

		// ポッチ作成
		var Geometry_Pochi = new THREE.BoxGeometry(BlockSize / 2.5, BlockSize / 2.5, BlockSize / 2.5);
		var Geometry_Pochi_Edge = new THREE.EdgesGeometry(Geometry_Pochi);
		var Mesh_Pochi, Edge_Pochi;
		var PosX, PosY, PosZ;
		for (var i = 0; i < w; i++) {
			for (var j = 0; j < d; j++) {
				PosX = (BlockSize * w / -2) + BlockSize * (i + 0.5);
				PosY = BlockSize / 2 + (BlockSize / 2.5) / 2;
				PosZ = (BlockSize * d / -2) + BlockSize * (j + 0.5);
				
				Mesh_Pochi = new THREE.Mesh(Geometry_Pochi, Material);
				Mesh_Pochi.position.set(PosX, PosY, PosZ);
				Block.add(Mesh_Pochi);

				if (EdgeColor != 'none') {
					    Edge_Pochi = new THREE.LineSegments(Geometry_Pochi_Edge, Material_Edge);
						Edge_Pochi.position.set(PosX, PosY, PosZ);
						Block.add(Edge_Pochi);
				}
			};
		};
		
		delete Geometry_Base;
		delete Geometry_Pochi_Edge;
		delete Geometry_Base;
		delete Geometry_Pochi_Edge;
		delete Material;
		delete Material_Edge;
		
		delete Mesh_Base;
		delete Edge_Base;
		delete Mesh_Pochi;
		delete Edge_Pochi;
		
		return Block;
	};

	function RemoveModel() {
		for (var i in SceneObj) {
			scene.remove(SceneObj[i].object);
			
			for (var j in SceneObj[i].object.children) {
				if(SceneObj[i].object.children[j].geometry) {
					SceneObj[i].object.children[j].geometry.dispose();
				};
				if(SceneObj[i].object.children[j].material) {
					SceneObj[i].object.children[j].material.dispose();
				};
				if(SceneObj[i].object.children[j].mesh) {
					SceneObj[i].object.children[j].mesh.dispose();
				};
				if(SceneObj[i].object.children[j].texture) {
					SceneObj[i].object.children[j].texture.dispose();
				};

				delete  SceneObj[i].object.children[j];
			};
			
			delete  SceneObj[i].object;
			delete  SceneObj[i];
		};
		
		SceneObj.length = 0;
		PartsName.length = 0;
	};

	function ChangeBlockVisible() {
		var LayerB = Number($('#LayerNumButtom').val());
		var LayerT = Number($('#LayerNumTop').val());
		var Parts = [];
		$('[name=ViewPartsSelect]:checked').each(function() {
			Parts.push($(this).val());
		});

		if (LayerB > LayerT) {
			var tmp = LayerB;
			LayerB = LayerT;
			LayerT = tmp;
		};

		for (var i in SceneObj) {
			if (SceneObj[i].type == 'Block') {
				if (SceneObj[i].layer >= LayerB && SceneObj[i].layer <= LayerT && $.inArray(SceneObj[i].parts, Parts) != -1) {
					SceneObj[i].object.visible = true;
				} else {
					SceneObj[i].object.visible = false;
				};
			};
		};
	};

	function CheckColorExists(c) {
		if (!BlockColor[c]) {
			console.error('No Exists Color Setting : ' + c);
		};
	};

	$('#LayerNumButtom').change(function() {
	    ChangeBlockVisible();
	});
	$('#LayerNumTop').change(function() {
	    ChangeBlockVisible();
	});
	$('#ViewParts').change(function() {
	    ChangeBlockVisible();
	});

	$('#Renew').click(function() {
		RemoveModel();
		
		EdgeColor = $('#EdgeColor').val();
		SetModelData();
	});

	$('#Remove').click(function() {
		RemoveModel();
	});

	$('#CameraToggle').click(function() {
		setCamera();
	});
});
