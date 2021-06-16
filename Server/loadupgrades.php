<?php
include "connection.php";

//Checking if data exists
$stmt = $conn->prepare("SELECT * FROM upgrades");
$stmt->execute();
$result = $stmt->fetchAll(PDO::FETCH_ASSOC);

//Sending back the upgrade result if it exists
echo json_encode($result);





?>