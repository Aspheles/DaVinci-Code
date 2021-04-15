<?php
include "connection.php";
$stmt = $conn->prepare("SELECT * FROM accounts WHERE email=:email");
$stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
$stmt->execute();
$result = $stmt->fetchAll(PDO::FETCH_ASSOC);
if ($stmt->rowCount() > 0) {
    if ($_POST['verificationcode'] == $result[0]['token']) {
        if (date("Y-m-d H:i:s") < $result[0]['expiredate']) {
            try {
                $stmt = $conn->prepare("UPDATE accounts SET password=:newpassword WHERE email=:email");
                $stmt->bindParam(":newpassword", password_hash($_POST['newpassword'], PASSWORD_DEFAULT), PDO::PARAM_STR);
                $stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
                $stmt->execute();
                array_push($result, (object)["status" => "success"]);
                echo json_encode($result);
            }
            catch(PDOException $e) {
                echo $e;
                exit($e->getMessage());
            }
        } else {
            array_push($result, (object)["status" => "errortokenexpire"]);   
            echo json_encode($result);
        }
    } else {
        array_push($result, (object)["status" => "errortokeninvalid"]);
        echo json_encode($result);
    }
}else{
    array_push($result, (object)["status" => "erroremailinvalid"]);
    echo json_encode($result);
}



?>

