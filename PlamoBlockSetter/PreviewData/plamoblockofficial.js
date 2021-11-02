$(function () {
	var width = 500;
	var height = 600;

	var scene, renderer;
	var camera, cameraA, cameraB;
	var lightA, lightB;

	var ModelVersion = 0;

	var BlockSize = 2;
	var PlateSize;
	var OffsetY;
	var BlockColor;

	var ModelName;
	// EdgeColor edge … none=境界線なし / black=黒 / gray=灰色 / egde=BlockColor.edgeの値
	var EdgeColor;

	var BlockModelList = {};
	var BlockGeometryList = {};
	var BlockMaterialList = {};

	var SceneObj = [];
	var PartsName = [];

	var controls;

	//パラメーター
	var arg = new Object;
	var pair = location.search.substring(1).split('&');
	for (var i = 0; pair[i]; i++) {
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

		scene = new THREE.Scene();

		var fov = 60;
		var aspect = width / height;
		var scale = 0.1;
		var near = 1;
		var far = 1000;
		// 平行投影
		cameraA = new THREE.OrthographicCamera(-width / 2 * scale, width / 2 * scale, height / 2 * scale, -height / 2 * scale, near, far);
		cameraA.position.set(25, 10, 50);
		// 透視投影
		cameraB = new THREE.PerspectiveCamera(fov, aspect, near, far);
		cameraB.position.set(25, 10, 50);

		var light;
		// 手前・右上から
		// light = new THREE.DirectionalLight(0xffffff);
		// light.position.set(1, 1, 1).normalize();
		// scene.add(light);
		// 奥・左上から
		// light = new THREE.DirectionalLight(0xffffff, 0.1);
		// light.position.set(-1, 1, -1).normalize();
		// scene.add(light);
		// 全体
		lightA = new THREE.AmbientLight(0xffffff);
		scene.add(lightA);
		lightB = new THREE.AmbientLight(0x888888);
		scene.add(lightB);

		/* 補助線
		var axis = new THREE.AxisHelper(1000);
		axis.position.set(0,0,0);
		scene.add(axis);
		var grid = new THREE.GridHelper(50, 2); // size, step
		scene.add(grid);
		*/

		// $.ajaxSetup({ cache: false });

		// ブロックの色の読み込み
		// $.ajaxSetup({ async: false });
		BlockColor = gblBlockColor;
		//$.ajaxSetup({ async: true });

		SetModelData();

		renew();
	};

	function renew() {
		renderer = new THREE.WebGLRenderer({ antialias: true });
		renderer.setSize(width, height);
		renderer.setClearColor(0xDDDDDD, 1);

		var doc = document.getElementById('PlamoBlock');
		while (doc.childNodes.length > 0) {
			doc.removeChild(doc.firstChild);
		};
		document.getElementById('PlamoBlock').appendChild(renderer.domElement);

		setLight(false);

		setCamera(false);
	};

	function setCamera(toggle) {
		if (!camera) {
			camera = cameraA;
		} else {
			if (toggle) {
				if (camera == cameraA) {
					camera = cameraB;
				} else if (camera == cameraB) {
					camera = cameraA;
				};
			};
		};

		if (controls) controls.dispose();
		controls = new THREE.OrbitControls(camera, renderer.domElement);
		controls.update();
	}

	function setLight(IsHighlight) {
		lightA.visible = !IsHighlight;
		lightB.visible = !lightA.visible;
	}

	function animate() {
		requestAnimationFrame(animate);

		controls.update();
		renderer.render(scene, camera);
	};

	/* キャラデータ配置 */
	function SetModelData() {
		var start_time = (new Date()).getTime();

		var ModelData;
		var Block;
		var LenParts, LenLayer, LenBlock;
		var l, MaxLayer;
		var BlockCount = {};
		var BlockType = ['1x1', '1x2', '1x3', '1x4', '1x6', '1x8', '2x2', '2x3', '2x4', '2x6', '2x8'];
		var buf;

		// キャラクターデータの読み込み
		// $.ajaxSetup({ async: false });
		ModelData = gblModelData;
		// $.ajaxSetup({ async: true });
		console.log(ModelData)
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
		OffsetY = MaxLayer * 0.4 * -1 - 9;

		// プレート設置
		CheckColorExists(obj.Plate[2]);
		PartsName[0] = '[ベース]';

		PlateSize = [Number(obj.Plate[0]), Number(obj.Plate[1])];

		var Plate = new MakeBlock(PlateSize[0], PlateSize[1], obj.Plate[2], false);
		Plate.position.y = BlockSize * (-1 + OffsetY);
		Plate.visible = true;
		scene.add(Plate);
		var PlateH = new MakeBlock(PlateSize[0], PlateSize[1], obj.Plate[2], true);
		PlateH.position.y = BlockSize * (-1 + OffsetY);
		PlateH.visible = false;
		scene.add(PlateH);
		SceneObj.push({object : Plate, objectH : PlateH, type : 'Panel'});

		var Plate = new MakeBlock(PlateSize[0] + 2, PlateSize[1] + 2, obj.Plate[2], false);
		Plate.position.y = BlockSize * (-1 + OffsetY - 1);
		Plate.visible = true;
		scene.add(Plate);
		var PlateH = new MakeBlock(PlateSize[0] + 2, PlateSize[1] + 2, obj.Plate[2], true);
		PlateH.position.y = BlockSize * (-1 + OffsetY - 1);
		PlateH.visible = false;
		scene.add(PlateH);
		SceneObj.push({object : Plate, objectH : PlateH, type : 'Panel'});

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

		var BlockPhong;
		if (l == 10) {
			BlockPhong = true;
		} else {
			BlockPhong = false;
		};

		var Block = MakeBlock(w1, d1, c, false);
		Block.visible = true;
		var BlockH = MakeBlock(w1, d1, c, true);
		BlockH.visible = false;

		var pos = BlockPos(l - 1, x - 1, y - 1, w1, d1);
		Block.position.set(pos[0], pos[1], pos[2]);
		BlockH.position.set(pos[0], pos[1], pos[2]);

		scene.add(Block);
		scene.add(BlockH);
		SceneObj.push({object : Block, objectH : BlockH, type : 'Block', layer : l, parts : PartsName[PartsNo]});
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
	function MakeBlock(w, d, c, BlockPhong) {
		let BlockGroup = new THREE.Group();
		let Geometry_Block, Geometry_Edge;
		let Material_Block, Material_Edge;

		var IndexName = w + '|' + d + '|' + c + '|' + (BlockPhong ? '1' : '0') + '|' + EdgeColor;
		
		if (BlockModelList[IndexName]) {
			//return JSON.parse(JSON.stringify(BlockModelList[IndexName]));
			return BlockModelList[IndexName].clone();
		}

		// ブロックの色
		[Material_Block, Material_Edge] = BlockMaterial(c, BlockPhong);

		// ベース作成
		[Geometry_Block, Geometry_Edge] = BlockGeometry(w, d);

		const Mesh_Block = new THREE.Mesh(Geometry_Block, Material_Block);

		BlockGroup.add(Mesh_Block);

		if (EdgeColor != 'none') {
			const Mesh_Edge = new THREE.LineSegments(Geometry_Edge, Material_Edge);
			BlockGroup.add(Mesh_Edge);
		};

		delete Geometry_Block;
		delete Material_Block;
		delete Mesh_Block;
		delete Geometry_Edge;
		delete Material_Edge;
		delete Mesh_Edge;

		// console.log('Block JSON [' + w + '/' + d + '/' + c + '] :' + JSON.stringify(Block.toJSON()));

		BlockModelList[IndexName] = BlockGroup;

		//return JSON.parse(JSON.stringify(BlockModelList[IndexName]));
		return BlockGroup;
	};

	function BlockMaterial(c, BlockPhong) {
		let Material_Block;
		let bs, eg, op;

		var IndexName = c + '|' + (BlockPhong ? '1' : '0') + '|' + EdgeColor;
		if (BlockMaterialList[IndexName]) {
			return [JSON.parse(JSON.stringify(BlockMaterialList[IndexName][0])), JSON.parse(JSON.stringify(BlockMaterialList[IndexName][1]))];
		}

		// エッジカラー設定
		bs = BlockColor[c].base;
		op = BlockColor[c].opacity;

		if (BlockPhong == true) {
			if (bs != '#FF0000') {
				eg = '#FF0000';
			} else {
				eg = '#FFFFFF';
			};
		} else {
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
		};

		if (op == 1) {
			if (BlockPhong == true) {
				//Material_Block = new THREE.MeshPhongMaterial({color: bs, specular: '#FFFFFF', shininess: 120, refractionRatio: 10});
				Material_Block = new THREE.MeshPhongMaterial({ emissive: bs, color: '#000000', specular: '#000000', shininess: 120, refractionRatio: 100 });
			} else {
				Material_Block = new THREE.MeshLambertMaterial({ color: bs });
			};
		} else {
			if (BlockPhong == true) {
				Material_Block = new THREE.MeshPhongMaterial({ color: bs, opacity: op, transparent: true });
			} else {
				Material_Block = new THREE.MeshLambertMaterial({ color: bs, opacity: op, transparent: true });
			};
		};
		Material_Edge = new THREE.LineBasicMaterial({ color: eg, linewidth: 2 });

		BlockMaterialList[IndexName] = [Material_Block, Material_Edge];
		console.log(BlockMaterialList[IndexName][0])
		return [JSON.parse(JSON.stringify(BlockMaterialList[IndexName][0])), JSON.parse(JSON.stringify(BlockMaterialList[IndexName][1]))];
	}

	function BlockGeometry(w, d) {
		var IndexName = w + '|' + d;
		if (BlockGeometryList[IndexName]) {
			return [JSON.parse(JSON.stringify(BlockGeometryList[IndexName][0])), JSON.parse(JSON.stringify(BlockGeometryList[IndexName][1]))];
		}

		// ジオメトリ生成
		var geometry = new THREE.Geometry();
		var geoE = new THREE.Geometry();

		var wd, ht, dp;
		wd = BlockSize;
		ht = BlockSize;
		dp = BlockSize;

		var cnt = 1;

		for (let tw = 1; tw <= w; tw++) {
			for (let td = 1; td <= d; td++) {
				BlockGeometry_OneBlock(geometry, geoE, cnt, tw, td);
				cnt++;
			}
		}
		geometry.mergeVertices();

		BlockGeometry_Edge(geoE, w, d)

		//0,0が中心になるように移動
		var gofw = (w - 1) / 2 * BlockSize * -1;
		var gofd = (d - 1) / 2 * BlockSize * -1;

		for (var i = 0; i < geometry.vertices.length; i++) {
			geometry.vertices[i].x += gofw;
			geometry.vertices[i].z += gofd;
		}
		for (var i = 0; i < geoE.vertices.length; i++) {
			geoE.vertices[i].x += gofw;
			geoE.vertices[i].z += gofd;
		}

		// 法線ベクトルの自動計算
		geometry.computeFaceNormals();
		geoE.computeFaceNormals();
		//geometry.computeVertexNormals();
		//geoE.computeVertexNormals();

		BlockGeometryList[IndexName] = [new THREE.BufferGeometry().fromGeometry(geometry), geoE];
		return [JSON.parse(JSON.stringify(BlockGeometryList[IndexName][0])), JSON.parse(JSON.stringify(BlockGeometryList[IndexName][1]))];
	}

	function BlockGeometry_Edge(geoE, w, d) {
		var wd, ht, dp;
		wd = BlockSize;
		ht = BlockSize;
		dp = BlockSize;

		var ofw, ofd;
		ofw = (w - 1) * BlockSize;
		ofd = (d - 1) * BlockSize;

		//上面
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2, ofd + dp / 2));
		//底面
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2 * -1, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2 * -1, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2 * -1, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2 * -1, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2 * -1, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2 * -1, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2 * -1, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2 * -1, ofd + dp / 2));
		//側面
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2 * -1, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2 * -1, ofd + dp / 2));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2 * -1, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2, dp / 2 * -1));
		geoE.vertices.push(new THREE.Vector3(wd / 2 * -1, ht / 2 * -1, dp / 2 * -1));
	}
	function BlockGeometry_OneBlock(geometry, geoE, cnt, w, d) {
		var wd, ht, dp;
		wd = BlockSize;
		ht = BlockSize;
		dp = BlockSize;

		var ofw, ofd;
		ofw = (w - 1) * BlockSize;
		ofd = (d - 1) * BlockSize;

		var ofidx = (cnt - 1) * 24;

		//メイン立方体
		geometry.vertices.push(new THREE.Vector3(ofw + wd / 2 * -1, ht / 2, ofd + dp / 2)); // 0 上面
		geometry.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2, ofd + dp / 2)); // 1
		geometry.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2, ofd + dp / 2 * -1)); // 2
		geometry.vertices.push(new THREE.Vector3(ofw + wd / 2 * -1, ht / 2, ofd + dp / 2 * -1)); // 3
		geometry.vertices.push(new THREE.Vector3(ofw + wd / 2 * -1, ht / 2 * -1, ofd + dp / 2)); // 4 底面
		geometry.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2 * -1, ofd + dp / 2)); // 5
		geometry.vertices.push(new THREE.Vector3(ofw + wd / 2, ht / 2 * -1, ofd + dp / 2 * -1)); // 6
		geometry.vertices.push(new THREE.Vector3(ofw + wd / 2 * -1, ht / 2 * -1, ofd + dp / 2 * -1)); // 7
		//ポッチ
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));   // 8 上面
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));   // 9
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));   //10
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));   //11
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2, ofd + 0.5 * BlockSize / 2.5));   //12 底面
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2, ofd + 0.5 * BlockSize / 2.5));   //13
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2, ofd + 0.5 * BlockSize / 2.5 * -1));   //14
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2, ofd + 0.5 * BlockSize / 2.5 * -1));   //15
		//ポッチ穴
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));   //16 上面
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));   //17
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));   //18
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));   //19
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5));   //20 底面
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5));   //21
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5 * -1));   //22
		geometry.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5 * -1));   //23

		//メイン立方体 上面
		geometry.faces.push(new THREE.Face3(ofidx + 0, ofidx + 1, ofidx + 12));
		geometry.faces.push(new THREE.Face3(ofidx + 1, ofidx + 13, ofidx + 12));
		geometry.faces.push(new THREE.Face3(ofidx + 1, ofidx + 2, ofidx + 13));
		geometry.faces.push(new THREE.Face3(ofidx + 14, ofidx + 13, ofidx + 2));
		geometry.faces.push(new THREE.Face3(ofidx + 2, ofidx + 3, ofidx + 14));
		geometry.faces.push(new THREE.Face3(ofidx + 14, ofidx + 3, ofidx + 15));
		geometry.faces.push(new THREE.Face3(ofidx + 15, ofidx + 3, ofidx + 0));
		geometry.faces.push(new THREE.Face3(ofidx + 15, ofidx + 0, ofidx + 12));

		//メイン立方体 側面1～4
		geometry.faces.push(new THREE.Face3(ofidx + 4, ofidx + 1, ofidx + 0));
		geometry.faces.push(new THREE.Face3(ofidx + 4, ofidx + 5, ofidx + 1));
		geometry.faces.push(new THREE.Face3(ofidx + 2, ofidx + 1, ofidx + 5));
		geometry.faces.push(new THREE.Face3(ofidx + 5, ofidx + 6, ofidx + 2));
		geometry.faces.push(new THREE.Face3(ofidx + 6, ofidx + 3, ofidx + 2));
		geometry.faces.push(new THREE.Face3(ofidx + 6, ofidx + 7, ofidx + 3));
		geometry.faces.push(new THREE.Face3(ofidx + 7, ofidx + 0, ofidx + 3));
		geometry.faces.push(new THREE.Face3(ofidx + 7, ofidx + 4, ofidx + 0));

		//メイン立方体 底面
		geometry.faces.push(new THREE.Face3(ofidx + 7, ofidx + 6, ofidx + 23));
		geometry.faces.push(new THREE.Face3(ofidx + 23, ofidx + 6, ofidx + 22));
		geometry.faces.push(new THREE.Face3(ofidx + 22, ofidx + 6, ofidx + 5));
		geometry.faces.push(new THREE.Face3(ofidx + 5, ofidx + 21, ofidx + 22));
		geometry.faces.push(new THREE.Face3(ofidx + 21, ofidx + 5, ofidx + 4));
		geometry.faces.push(new THREE.Face3(ofidx + 4, ofidx + 20, ofidx + 21));
		geometry.faces.push(new THREE.Face3(ofidx + 20, ofidx + 4, ofidx + 7));
		geometry.faces.push(new THREE.Face3(ofidx + 7, ofidx + 23, ofidx + 20));

		//ポッチ 上面
		geometry.faces.push(new THREE.Face3(ofidx + 8, ofidx + 9, ofidx + 10));
		geometry.faces.push(new THREE.Face3(ofidx + 10, ofidx + 11, ofidx + 8));

		//ポッチ 側面面
		geometry.faces.push(new THREE.Face3(ofidx + 8, ofidx + 12, ofidx + 9));
		geometry.faces.push(new THREE.Face3(ofidx + 12, ofidx + 13, ofidx + 9));
		geometry.faces.push(new THREE.Face3(ofidx + 9, ofidx + 13, ofidx + 10));
		geometry.faces.push(new THREE.Face3(ofidx + 13, ofidx + 14, ofidx + 10));
		geometry.faces.push(new THREE.Face3(ofidx + 10, ofidx + 14, ofidx + 11));
		geometry.faces.push(new THREE.Face3(ofidx + 14, ofidx + 15, ofidx + 11));
		geometry.faces.push(new THREE.Face3(ofidx + 11, ofidx + 15, ofidx + 8));
		geometry.faces.push(new THREE.Face3(ofidx + 8, ofidx + 15, ofidx + 12));

		//ポッチ穴 底面
		geometry.faces.push(new THREE.Face3(ofidx + 16, ofidx + 19, ofidx + 17));
		geometry.faces.push(new THREE.Face3(ofidx + 17, ofidx + 19, ofidx + 18));

		//ポッチ穴 側面面
		geometry.faces.push(new THREE.Face3(ofidx + 19, ofidx + 16, ofidx + 20));
		geometry.faces.push(new THREE.Face3(ofidx + 20, ofidx + 23, ofidx + 19));
		geometry.faces.push(new THREE.Face3(ofidx + 19, ofidx + 23, ofidx + 18));
		geometry.faces.push(new THREE.Face3(ofidx + 18, ofidx + 23, ofidx + 22));
		geometry.faces.push(new THREE.Face3(ofidx + 18, ofidx + 22, ofidx + 17));
		geometry.faces.push(new THREE.Face3(ofidx + 17, ofidx + 22, ofidx + 21));
		geometry.faces.push(new THREE.Face3(ofidx + 17, ofidx + 21, ofidx + 16));
		geometry.faces.push(new THREE.Face3(ofidx + 16, ofidx + 21, ofidx + 20));

		//ポッチ（線用）
		//上面
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		//底面
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2, ofd + 0.5 * BlockSize / 2.5));

		//側面
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2, ofd + 0.5 * BlockSize / 2.5 * -1));

		//ポッチ穴（線用）
		//上面
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		//底面
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5));

		//側面
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1 + BlockSize / 2.5, ofd + 0.5 * BlockSize / 2.5 * -1));
		geoE.vertices.push(new THREE.Vector3(ofw + 0.5 * BlockSize / 2.5 * -1, ht / 2 * -1, ofd + 0.5 * BlockSize / 2.5 * -1));
	}

	function RemoveModel() {
		for (var i in SceneObj) {
			scene.remove(SceneObj[i].object);

			for (var j in SceneObj[i].object.children) {
				if (SceneObj[i].object.children[j].geometry) {
					SceneObj[i].object.children[j].geometry.dispose();
				};
				if (SceneObj[i].object.children[j].material) {
					SceneObj[i].object.children[j].material.dispose();
				};
				if (SceneObj[i].object.children[j].mesh) {
					SceneObj[i].object.children[j].mesh.dispose();
				};
				if (SceneObj[i].object.children[j].texture) {
					SceneObj[i].object.children[j].texture.dispose();
				};

				delete SceneObj[i].object.children[j];
			};

			delete SceneObj[i].object;
			delete SceneObj[i];
		};

		SceneObj.length = 0;
		PartsName.length = 0;
	};

	function CheckColorExists(c) {
		if (!BlockColor[c]) {
			console.error('No Exists Color Setting : ' + c);
		};
	};
});
