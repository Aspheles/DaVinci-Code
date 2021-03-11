<?php include "connection.php";

try{
    $stmt = $conn->prepare("SELECT * FROM accounts where email=:email");
    $stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

    if($stmt->rowCount() > 0){
        //echo $result["token"] . "b" . $result["expiredate"] . "b" . $result["verified"];
        if($_POST['code'] == $result[0]['token']){
            if(date("Y-m-d H:i:s") < $result[0]['expiredate']){
                try{
                    $stmt = $conn->prepare("UPDATE accounts SET token='0', verified='1' WHERE email=:email");
                    $stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
                    $stmt->execute();
                    
                    array_push($result, (object)["status" => "success"]);
            
                }
                catch (PDOException $e) {     
                    exit($e->getMessage());  
                }
            }else{
                array_push($result, (object)["status" => "errorcodeexpired"]);
            }
        }else{
            array_push($result, (object)["status" => "errorcodeinvalid"]);
        }
    }

    echo json_encode($result);
}
catch (PDOException $e) {     
    exit($e->getMessage());  
}

?>