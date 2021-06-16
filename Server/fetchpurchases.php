<?php
include "connection.php";


try{
    //Checking if data exists
    $stmt = $conn->prepare("SELECT * FROM purchases WHERE playerid=:accountid");
    $stmt->bindParam(":accountid", $_POST['accountid']);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

    if(sizeof($result) > 0){
        array_push($result, (object)["status" => "Found Upgrades"]);
    }else{
        array_push($result, (object)["status" => "No Found Upgrades"]);
    }
    //Sending back the upgrade result if it exists
    echo json_encode($result);
}catch(PDOException $err){
    exit($e->getMessage());  
}






?>