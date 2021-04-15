<?php
include "connection.php";
$title = preg_replace('/[\x{200B}-\x{200D}]/u', '', $_POST['title']);

//Checking if ids match
$stmt = $conn->prepare("SELECT * FROM puzzles WHERE id=:id");
$stmt->bindParam(":id", $_POST['id'], PDO::PARAM_STR);
$stmt->execute();
$result = $stmt->fetchAll(PDO::FETCH_ASSOC);

if($stmt->rowCount() > 0){
    //Checking if name matches and is not the current id
    $stmt = $conn->prepare("SELECT * FROM puzzles WHERE name=:title AND id!=:id");
    $stmt->bindParam(":title", $title);
    $stmt->bindParam(":id", $_POST['id'], PDO::PARAM_STR);
    $stmt->execute();

    if($stmt->rowCount() > 0){
        array_push($result, (object)[
            "status" => "errorduplicate"
        ]);

        echo json_encode($result);
    }else{
            //Updating question with new values
            $query = $conn->prepare("UPDATE puzzles SET name=:title, description=:description, difficulty=:difficulty WHERE id=:id");
            $query->bindParam(":id", $_POST["id"]);
            $query->bindParam(":title", $title);
            $query->bindParam(":description", $_POST["description"]);
            $query->bindParam(":difficulty", $_POST["difficulty"]);
            $query->execute();
            
            array_push($result, (object)[
                "status" => "success"
            ]);
            echo json_encode($result);
    }

}


?>