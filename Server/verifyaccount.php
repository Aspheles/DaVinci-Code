<?php include "connection.php";

try{
    $stmt = $conn->prepare("UPDATE accounts SET token='0', verified='1' WHERE email=:email");
    $stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
    $stmt->execute();
    $result = $stmt->fetch(PDO::FETCH_ASSOC);

    if($stmt->rowCount() > 0){
        array_push($result, (object)["status" => "success"]);
    }

    echo json_encode($result);
}
catch (PDOException $e) {     
    exit($e->getMessage());  
}

?>