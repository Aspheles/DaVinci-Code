<?php
include "connection.php";


try{
    //Check if user already bough power up
    $stmt = $conn->prepare("DELETE FROM purchases WHERE playerid=:playerid AND upgradeid=:upgradeid");
    $stmt->bindParam(":playerid", $_POST['accountid']);
    $stmt->bindParam(":upgradeid", $_POST['upgradeid']);
    $stmt->execute();
    

    array_push($result, (object)["status" => "Upgrade has been removed"]);



    echo json_encode($result);
}catch(PDOException $err){
    exit($e->getMessage());  
}





?>