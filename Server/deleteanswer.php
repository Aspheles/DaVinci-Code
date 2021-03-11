<?php
include "connection.php";

try {
    $stmt = $conn->prepare("DELETE FROM answers WHERE id=:id");
    $stmt->bindParam(":id", $_POST['id'], PDO::PARAM_STR);
    $stmt->execute();

} catch (PDOException $err) {
    
}



?>