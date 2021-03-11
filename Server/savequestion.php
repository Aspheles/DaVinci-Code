<?php
include "connection.php";

//Checking for question first

$title = $_POST['question_title'];
$description = $_POST['question_description'];
//$image = $_POST['question_image'];
$puzzleid = $_POST['puzzle_id'];


$stmt = $conn->prepare("SELECT * FROM questions WHERE id=:id AND title=:title AND puzzle_id =:puzzle_id");
$stmt->bindParam(":id", $_POST['question_id']);
$stmt->bindParam(":title", $_POST['question_title']);
$stmt->bindParam(":puzzle_id", $_POST['puzzle_id']);
$stmt->execute();
$result = $stmt->fetchAll(PDO::FETCH_ASSOC);

if($stmt->rowCount() > 0){
    //Checking if name matches and is not the current id
    $stmt = $conn->prepare("SELECT * FROM questions WHERE title=:title AND id!=:id");
    $stmt->bindParam(":title", $_POST['question_title']);
    $stmt->bindParam(":id", $_POST['question_id']);
    $stmt->execute();

    if($stmt->rowCount() > 0){
        array_push($result, (object)[
        "status" => "errorduplicate"
        ]);

        echo json_encode($result);
    }else{
       
       //Update Data of current question
        $stmt = $conn->prepare("UPDATE questions SET title=:title, description=:description WHERE id=:id");
        $stmt->bindParam(":id", $_POST['question_id']);
        $stmt->bindParam(":title", $_POST['question_title']);
        $stmt->bindParam(":description", $_POST['question_description']);
        $stmt->execute();

        array_push($result, (object)[
            "status" => "success"
        ]);
        echo json_encode($result);

        // $stmt = $conn->prepare("SELECT * FROM questions WHERE id=:id");
        // $stmt->bindParam(":id", $_POST['question_id']);
        // $stmt->execute();
        // $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

    }
}else{
        //Checking if name matches and is not the current id
		$stmt = $conn->prepare("SELECT * FROM questions WHERE title=:title AND id!=:id");
		$stmt->bindParam(":title", $_POST['question_title']);
		$stmt->bindParam(":id", $_POST['question_id']);
		$stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
		
		if($stmt->rowCount() > 0){
        array_push($result, (object)[
        "status" => "errorduplicate"
        ]);

        echo json_encode($result);
		}else{
		
		//Create new Question
        $stmt = $conn->prepare("INSERT INTO questions(title, description, puzzle_id) values (:title, :description, :puzzle_id)");
        $stmt->bindParam(":title", $title);
        $stmt->bindParam(":description", $description);
        $stmt->bindParam(":puzzle_id", $puzzleid);
        $stmt->execute();

        if($stmt){
            $stmt = $conn->prepare("SELECT * FROM questions WHERE title=:title");
            $stmt->bindParam(":title", $title);
            $stmt->execute();
            $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

            if($stmt->rowCount() > 0){
                
                array_push($result, (object)[
                    "status" => "success",
                    "last_id" => $result[0]['id']
                ]);

                echo json_encode($result);
            }
        }
		
		
		}
		
		
		
		
    
}



?>