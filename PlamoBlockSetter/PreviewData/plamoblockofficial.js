$(function () {
	var scene, renderer;
	var camera, cameraA, cameraB;
	
	var ModelVersion = 0;

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
		EdgeColor = 'edge';
		
		scene = new THREE.Scene();
		
		var width  = 600;
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
		BlockColor = gblBlockColor;
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
		ModelData = gblModelData;
		$.ajaxSetup({async: true});

		// データ形式のバージョンを取得
		if ('Version' in ModelData) {
            ModelVersion = ModelData.Version;
		};
		
		var obj = ModelData.Chara;

		// 最大の高さ取得
		MaxLayer = 1;
		LenParts = obj.Block.length;
		for (var i = 1; i < LenParts + 1; i++) {
			l = Number(obj.Block[i - 1].BottomPos);
			for (var j = 0; j < LenLayer; j++) {
				MaxLayer = Math.max(l, MaxLayer);
				l += 1;
			};
		};

		// オフセット設定
		// OffsetY = MaxLayer * 0.4 * -1;
		OffsetY =  MaxLayer * 0.4 * -1 - 9;

		// プレート設置
		CheckColorExists(obj.Plate[2]);
		PartsName[0] = '[ベース]';
		
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
		for (var i = 1; i < LenParts + 1; i++) {
			PartsName[i] = obj.Block[i - 1].Name;
			
			// パーツの最底面の位置
			l = Number(obj.Block[i - 1].BottomPos);
			
			// 層ごとに処理
			LenLayer = obj.Block[i - 1].Layer.length;
			for (var j = 0; j < LenLayer; j++) {
				MaxLayer = Math.max(l, MaxLayer);
			
				// ブロックごとに処理
				LenBlock = obj.Block[i - 1].Layer[j].length;
				for (var k = 0; k < LenBlock; k++) {
					Block = obj.Block[i - 1].Layer[j][k];
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
		var bufX, bufY;
	
		bufX = x;
		bufY = y;
		if (ModelVersion >= 1) {
			bufX += (PlateSize[0] / 2 + 1);
			bufY += (PlateSize[1] / 2 + 1);
		};

		// 起点計算
		PosX = (BlockSize * PlateSize[0] / -2) + BlockSize * (bufX + 0.5);
		PosY = (BlockSize * PlateSize[1] / -2) + BlockSize * (bufY + 0.5);
		PosL = BlockSize * (l + OffsetY);
		
		// ブロックサイズ分ずらす
		PosX += BlockSize * (w / 2 - 0.5);
		PosY += BlockSize * (d / 2 - 0.5);
		
		return [PosX, PosL, PosY];
	};

	// ブロック作成
	function MakeBlock(w, d, c) {
		const BlockGroup = new THREE.Group();
		const Block = new THREE.Geometry();
		const Matrix = new THREE.Matrix4();
		let Material_Block, Material_Edge
		let bs, eg, op
		
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
			Material_Block = new THREE.MeshPhongMaterial({color: bs});
		} else {
			Material_Block = new THREE.MeshPhongMaterial({color: bs, opacity: op, transparent: true});
		};
		Material_Edge = new THREE.LineBasicMaterial({color: eg, linewidth: 2});
		
		// ベース作成
		const Block_Base = new THREE.BoxGeometry(BlockSize * w, BlockSize, BlockSize * d);
		Matrix.makeTranslation(0, 0, 0);	
		Block.merge(Block_Base, Matrix);

		// ポッチ作成
		const Block_Pochi = new THREE.BoxGeometry(BlockSize / 2.5, BlockSize / 2.5, BlockSize / 2.5);
		let PosX, PosY, PosZ;
		for (let i = 0; i < w; i++) {
			for (let j = 0; j < d; j++) {
				PosX = (BlockSize * w / -2) + BlockSize * (i + 0.5);
				PosY = BlockSize / 2 + (BlockSize / 2.5) / 2;
				PosZ = (BlockSize * d / -2) + BlockSize * (j + 0.5);
				
				Matrix.makeTranslation(PosX, PosY, PosZ);	
				Block.merge(Block_Pochi, Matrix);
			};
		};

		Block.mergeVertices();
		const Mesh_Block = new THREE.Mesh(Block, Material_Block);
		BlockGroup.add(Mesh_Block);

		if (EdgeColor != 'none') {
			const Edge = new THREE.EdgesGeometry(Block);
			const Mesh_Edge = new THREE.LineSegments(Edge, Material_Edge);
			BlockGroup.add(Mesh_Edge);
		};
		
		delete Block;
		delete Block_Base;
		delete Block_Pochi;
		delete Material_Block;
		delete Mesh_Block;
		delete Edge;
		delete Material_Edge;
		delete Mesh_Edge;

		// console.log('Block JSON [' + w + '/' + d + '/' + c + '] :' + JSON.stringify(Block.toJSON()));

		return BlockGroup;
	};

	function CheckColorExists(c) {
		if (!BlockColor[c]) {
			console.error('No Exists Color Setting : ' + c);
		};
	};
});
