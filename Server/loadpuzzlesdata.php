<?php
include "connection.php";

try{
    $stmt = $conn->prepare("SELECT *  FROM puzzles");
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
    echo json_encode($result);
    

}
catch (PDOException $e) {     
    exit($e->getMessage());  
}





?>