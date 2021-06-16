<?php
include "connection.php";


try{
    //Check if user already bough power up
    $stmt = $conn->prepare("SELECT * FROM purchases WHERE playerid=:playerid AND upgradeid=:upgradeid ");
    $stmt->bindParam(":playerid", $_POST['accountid']);
    $stmt->bindParam(":upgradeid", $_POST['upgradeid']);
    $stmt->execute();
    $result = $stmt->fetchAll();

    if(sizeof($result) > 0){
        array_push($result, (object)["status" => "Powerup has already been purchased"]);
    }else{

        $stmt = $conn->prepare("INSERT INTO purchases(playerid, upgradeid, level) values (:playerid, :upgradeid, :level)");
        $stmt->bindParam(":playerid", $_POST['accountid']);
        $stmt->bindParam(":upgradeid", $_POST['upgradeid']);
        $stmt->bindParam(":level", $_POST['level']);
        $stmt->execute();
    
        array_push($result, (object)["status" => "Purchase has been completed"]);


        //Reduce money of user in db
        $price = $_POST['price'];

        $stmt = $conn->prepare("UPDATE money SET currency= currency-$price WHERE accountid=:accountid");
        $stmt->bindParam(":accountid", $_POST['accountid']);
        $stmt->execute();
    }



    

    echo json_encode($result);
}catch(PDOException $err){
    exit($e->getMessage());  
}





?>