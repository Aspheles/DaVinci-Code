<?php
include "connection.php";
$difficulty = $_POST['difficulty'];

if (empty(!$difficulty)) {
    try {
        $stmt = $conn->prepare("SELECT * FROM puzzles WHERE difficulty=:difficulty");
        $stmt->bindParam(":difficulty", $_POST['difficulty'], PDO::PARAM_STR);
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

        echo json_encode($result);
    } catch (PDOException $e) {
        exit($e->getMessage());
    }
} else {
    try {
        $stmt = $conn->prepare("SELECT * FROM puzzles");
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        echo json_encode($result);
    } catch (PDOException $e) {
        exit($e->getMessage());
    }
}

?>
