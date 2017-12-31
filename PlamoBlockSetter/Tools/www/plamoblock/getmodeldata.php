<?php

$link = mysql_connect('mysql327.db.sakura.ne.jp', 'kytomtom', 'puresia1062');
if (!$link) {
    die('DB Connect Error: '.mysql_error());
}

$DataID = $_GET["id"];

// MySQLに対する処理
$db_selected = mysql_select_db('kytomtom_plamoblock', $link);
if (!$db_selected){
    die('DB Select Error: '.mysql_error());
}

mysql_set_charset('utf8');
$result = mysql_query("SELECT * FROM ModelData WHERE ID='" . $DataID . "'");
if (!$result) {
    die('DB Query Error: '.mysql_error());
}

$row = mysql_fetch_assoc($result);
if (!$row) {
    die('DB Data Error: '.mysql_error());
}

print($row['Data']);

$close_flag = mysql_close($link);

?>
