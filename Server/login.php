<?php
include "connection.php";


if(!empty($_POST['email']) && !empty($_POST['password'])){
    try{
        $stmt = $conn->prepare("SELECT id, username, email, classcode, password, isadmin, verified  FROM accounts WHERE email=:email");
        $stmt->bindParam(":email", $_POST['email'], PDO::PARAM_STR);
        $stmt->execute();
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

        if($stmt->rowCount() > 0){
            if(password_verify($_POST['password'], $result[0]["password"])){
                array_push($result, (object)["status" => "success"]);
            }else{
                // echo "Error: username or password is incorrect";
                array_push($result, (object)["status" => "erroruser"]);
            }    
        }else{
            // echo "Error: username or password is incorrect";
            array_push($result, (object)["status" => "erroruser"]);
        }
    }
    catch (PDOException $e) {     
        exit($e->getMessage());  
    }

}else{
    //echo "Email or password can't be empty";
    array_push($result, (object)["status" => "erroruserempty"]);
}

echo json_encode($result);


?>