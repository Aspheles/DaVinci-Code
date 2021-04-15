<?php include "connection.php";

$verificationCode = mt_rand(100000,999999);
$time = date("Y-m-d H:i:s", strtotime("+5 min"));

try{
    $stmt = $conn->prepare("UPDATE accounts SET token='$verificationCode', expiredate='$time' WHERE email=:email");
    $stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
    $stmt->execute();

    $stmt = $conn->prepare("SELECT * FROM accounts where email=:email");
    $stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

    if($stmt->rowCount() > 0){
       //echo "Success" . "b" n. $result['username'] . "b" . $result['email'] . "b" . $result["token"] . "b" . $result["verified"] . "b" . $result["expiredate"];
       array_push($result, (object)["status" => "success"]);
       mail($_POST['email'], "Hello " . $result[0]['name'] . ", your new verification code: ", $verificationCode);
    }else{
        array_push($result, (object)["status" => "erroraccountinvalid"]);
        //echo "Account doesn't exist";
    }
	
	echo json_encode($result);
}
catch (PDOException $e) {     
    exit($e->getMessage());  
}

?>