<?php
ini_set('display_errors', 1); // set to 0 for production version
error_reporting(E_ALL);
$myfile = fopen("code.cs", "w") or die("Unable to open file!");
$txt = $_POST['cstext'];
fwrite($myfile, $txt);
fclose($myfile);
echo "writed";
echo '<a href="index.html">';
echo '   <input type="button" value="Back" />';
echo '</a>';
?>