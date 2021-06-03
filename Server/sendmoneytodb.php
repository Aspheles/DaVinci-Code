<?php 
include "connection.php";

//Checking if the fetched query has data back from the money which exceed 0
function HasMoney($size){
    
    if(sizeof($size) > 0) return true;
    else return false;
   
}

//Inserting data to money so it can be saved
function InsertMoney($conn){
    $stmt = $conn->prepare("INSERT INTO money(accountid, currency) values (:accountid, :currency)");
    $stmt->bindParam(":accountid", $_POST['accountid']);
    $stmt->bindParam(":currency", $_POST['money']);
    $stmt->execute();
}

//Checking if data exists
$stmt = $conn->prepare("SELECT * FROM money WHERE accountid=:accountid");
$stmt->bindParam(":accountid", $_POST['accountid']);
$stmt->execute();
$result = $stmt->fetchAll(PDO::FETCH_ASSOC);

$hasMoney = HasMoney($result);

if($hasMoney){
    $receivedMoney = $_POST['money'];

    $stmt = $conn->prepare("UPDATE money SET currency= currency + $receivedMoney WHERE accountid=:accountid");
    $stmt->bindParam(":accountid", $_POST['accountid']);
    $stmt->execute();

    if($stmt) array_push($result, (object)["status" => "success updating money " . $receivedMoney]);
}else{
    //No data in database available
    InsertMoney($conn);
    array_push($result, (object)["status" => "success creating money"]);
}

echo json_encode($result);



?>