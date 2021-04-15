<?php

    include "connection.php";

    try
    {

        //Delete Answers linked to question
        $stmt = $conn->prepare("DELETE FROM answers WHERE question_id=:id");
        $stmt->bindParam(":id", $_POST['id']);
        $stmt->execute();
            
       //Delete Questions
       $stmt = $conn->prepare("DELETE FROM questions WHERE id=:id");
       $stmt->bindParam(":id", $_POST['id']);
       $stmt->execute();

    }
catch (PDOException $err) {
    echo $err;
}

?>