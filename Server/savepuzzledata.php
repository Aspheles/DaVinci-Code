<?php
include "connection.php";
$title = $_POST['title'];
$value = $_POST['value'] == "True" ? 1 : 0;

$stmt = $conn->prepare("SELECT * FROM answers WHERE id=:id and question_id=:question_id and title=:title");
$stmt->bindParam(":id", $_POST['answer_id']);
$stmt->bindParam(":title", $_POST['title']);
$stmt->bindParam(":question_id", $_POST['question_id']);
$stmt->execute();
$result = $stmt->fetchAll(PDO::FETCH_ASSOC);

if($stmt->rowCount() > 0){
    //Checking if name matches and is not the current id
    $stmt = $conn->prepare("SELECT * FROM answers WHERE title=:title AND id!=:id and question_id=:question_id");
    $stmt->bindParam(":title", $_POST['title']);
    $stmt->bindParam(":id", $_POST['answer_id']);
    $stmt->bindParam(":question_id", $_POST['question_id']);
    $stmt->execute();

    if($stmt->rowCount() > 0){
        array_push($result, (object)[
        "status" => "errorduplicate"
        ]);

        echo json_encode($result);
    }else{
        //Update values
        $stmt = $conn->prepare("UPDATE answers SET title='$title', value='$value' WHERE id=:id");
        $stmt->bindParam(":id", $_POST['answer_id']);
        $stmt->execute();

        array_push($result, (object)[
            "status" => "success"
        ]);
        echo json_encode($result);
    }
}else{
    //Checking if name matches and is not the current id
    $stmt = $conn->prepare("SELECT * FROM answers WHERE title=:title AND id!=:id and question_id=:question_id");
    $stmt->bindParam(":title", $_POST['title']);
    $stmt->bindParam(":id", $_POST['answer_id']);
    $stmt->bindParam(":question_id", $_POST['question_id']);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

    if($stmt->rowCount() > 0){
        array_push($result, (object)[
        "status" => "errorduplicate"
        ]);

        echo json_encode($result);
    }else{
        //Create the answer
        $stmt = $conn->prepare("INSERT INTO answers(title, value, question_id) values (:title, :value, :question_id)");
        $stmt->bindParam(":title", $title);
        $stmt->bindParam(":value", $value);
        $stmt->bindParam(":question_id", $_POST["question_id"]);
        $stmt->execute();

        if($stmt){
            $stmt = $conn->prepare("SELECT * FROM answers WHERE title=:title");
            $stmt->bindParam(":title", $_POST['title']);
            $stmt->execute();
            $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

            if($stmt->rowCount() > 0){
                array_push($result, (object)[
                    "status" => "success"
                ]);
                echo json_encode($result);
            }

        }

       

    }
}



?>