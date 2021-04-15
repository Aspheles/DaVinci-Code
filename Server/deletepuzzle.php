<?php

    include "connection.php";

    try
    {
        


        //Fetch question id
        $stmt = $conn->prepare("SELECT * FROM questions WHERE puzzle_id=:puzzle_id");
        $stmt->bindParam(":puzzle_id", $_POST['id']);
        $stmt->execute();
        $result = $stmt->fetchAll();

        for($i = 0; $i < count($result); $i++){
            //Delete Answers
            $stmt = $conn->prepare("DELETE FROM answers WHERE question_id=:question_id");
            $stmt->bindParam(":question_id", $result[$i]['id']);
            $stmt->execute();
        }

        

       //Delete Questions
       $stmt = $conn->prepare("DELETE FROM questions WHERE puzzle_id=:puzzle_id");
       $stmt->bindParam(":puzzle_id", $_POST['id']);
       $stmt->execute();


       //Delete Puzzle
       $query = $conn->prepare("DELETE FROM puzzles WHERE id=:id");
       $query->bindParam(":id", $_POST['id'], PDO::PARAM_STR);
       $query->execute();


     
         
    }
catch (PDOException $err) {
    echo $err;
}

?>