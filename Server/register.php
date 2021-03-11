<?php
include "connection.php";
$pw = password_hash($_POST["password"], PASSWORD_DEFAULT);
$tkn = mt_rand(100000, 999999);
$date = date("Y-m-d H:i:s", strtotime("+5 min"));
try {
    $stmt = $conn->prepare("SELECT * FROM accounts WHERE email=:email");
    $stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
    if ($stmt->rowCount() > 0) {
        array_push($result, (object)["status" => "errormailused"]);
        echo json_encode($result);
    } else {
        $stmt = $conn->prepare("INSERT INTO accounts(email, username, password, classcode, token, expiredate) values (:email, :username, :password, :class, :token, :expiredate)");
        $stmt->bindParam(':email', $_POST["email"], PDO::PARAM_STR, 255);
        $stmt->bindParam(':username', $_POST["username"], PDO::PARAM_STR, 255);
        $stmt->bindParam(':password', $pw);
        $stmt->bindParam(":class", $_POST['classcode']);
        $stmt->bindParam(":token", $tkn);
        $stmt->bindParam(":expiredate", $date);
        $stmt->execute();

        $stmt = $conn->prepare("SELECT * FROM accounts WHERE email=:email");
        $stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        
        array_push($result, (object)["status" => "success"]);
        echo json_encode($result);
        mail($_POST['email'], "Hello " . $result[0]['name'] . ", your register verification code: ", $tkn);
        //echo "Success" . "b" . $result['username'] . "b" . $result['email'] . "b" . $result["token"] . "b" . $result["verified"] . "b" . $result["expiredate"];
        
    }
}
catch(PDOException $e) {
    exit($e->getMessage());
}
