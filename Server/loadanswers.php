<?php
include "connection.php";

try{
    $stmt = $conn->prepare("SELECT *  FROM answers WHERE question_id=:id");
    $stmt->bindParam(":id", $_POST['id'], PDO::PARAM_STR);
    $stmt->execute();

    if($stmt->rowCount() > 0){
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        echo json_encode($result);
    }else{
        echo "Error No answers found";
    }
    

}
catch (PDOException $e) {     
    exit($e->getMessage());  
}





?>