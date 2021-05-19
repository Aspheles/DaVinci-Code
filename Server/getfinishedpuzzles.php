<?php
include "connection.php";


try {
    $stmt = $conn->prepare("SELECT * FROM completedpuzzles WHERE accountid=:accountid");
    $stmt->bindParam(":accountid", $_POST['accountid'], PDO::PARAM_STR);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

    echo json_encode($result);
} catch (PDOException $e) {
    exit($e->getMessage());
}


?>
