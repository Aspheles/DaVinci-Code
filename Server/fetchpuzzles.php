<?php

    include "connection.php";

    try
    {
        $query = $conn->prepare("SELECT * FROM puzzles");
        $query->execute();
        $result = $query->fetchAll(PDO::FETCH_ASSOC);
        echo json_encode($result);
    }
catch (PDOException $e) {     
    exit($e->getMessage());  
}
?>