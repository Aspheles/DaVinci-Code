<?php
include "connection.php";

try{
    $stmt = $conn->prepare("SELECT id, title, description, image  FROM questions WHERE puzzle_id=:puzzle_id");
    $stmt->bindParam(":puzzle_id", $_POST['puzzle_id'], PDO::PARAM_STR);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);


    echo json_encode($result);
    // foreach($result as $data){
    //     echo $data['id'] . " " . $data['title'] . ",";
    // }

    // $row = array();
    // while($row = $result){
    //     $rows[] = $row;
    // }


    // if($stmt->rowCount() > 0){
    //     echo $result['title'];
    // }else{
    //     echo "No questions found";
    // }
}
catch (PDOException $e) {     
    exit($e->getMessage());  
}





?>