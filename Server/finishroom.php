<?php
include "connection.php";

//Checking if the fetched query has data back from the completezpuzzles which exceed 0
function HasCompletedPuzzles($size){
    
    if($size > 0) return true;
    else return false;
   
}

//Inserting data to completedpuzzles so it can be saved
function InsertPuzzle($conn){
    $stmt = $conn->prepare("INSERT INTO completedpuzzles (puzzleid, accountid, runid) values (:puzzleid, :accountid, :runid)");
    $stmt->bindParam(":puzzleid", $_POST['puzzleid']);
    $stmt->bindParam(":accountid", $_POST['accountid']);
    $stmt->bindParam(":runid", $_POST['runid']);
    $stmt->execute();
}

//Checking if data exists
$stmt = $conn->prepare("SELECT * FROM completedpuzzles WHERE puzzleid=:puzzleid and accountid=:accountid");
$stmt->bindParam(":puzzleid", $_POST['puzzleid']);
$stmt->bindParam(":accountid", $_POST['accountid']);
$stmt->execute();
$result = $stmt->fetchAll(PDO::FETCH_ASSOC);

$isCompletedPuzzles = HasCompletedPuzzles($result);

if($isCompletedPuzzles){
    foreach ($result as $puzzle){
        //Checking that the puzzle is not in the database
        if($puzzle['puzzleid'] != $_POST['puzzleid']){
            InsertPuzzle($conn);
            array_push($result, (object)["status" => "success"]);
        }else{
            //Puzzle with the id already exists with the accountid
            array_push($result, (object)["status" => "error"]);
        }       
    }
}else{
    //No data in database available
    InsertPuzzle($conn);
    array_push($result, (object)["status" => "success"]);
}

echo json_encode($result);




?>






