<?php
include "connection.php";

//Checking if data exists
$stmt = $conn->prepare("SELECT * FROM money WHERE accountid=:accountid");
$stmt->bindParam(":accountid", $_POST['accountid']);
$stmt->execute();
$result = $stmt->fetchAll(PDO::FETCH_ASSOC);

if(sizeof($result) > 0){
    array_push($result, (object)["status" => "Found money"]);
}else{
    array_push($result, (object)["status" => "No Found money"]);
}
//Sending back the currency if it exists
echo json_encode($result);


?>