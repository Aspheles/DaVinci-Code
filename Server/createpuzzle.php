<?php
include "connection.php";

try
{
    $stmt = $conn->prepare("SELECT * FROM puzzles WHERE name=:name");
    $stmt->bindParam(":name", $_POST['name'], PDO::PARAM_STR);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

    if ($stmt->rowCount() > 0)
    {
        // echo "Error: The puzzle name is not available";
    
        array_push($result, (object)[
            "status" => "errorduplicate"
        ]);

        echo json_encode($result);
    }
    else
    {
        $stmt = $conn->prepare("INSERT INTO puzzles(name, difficulty, description, creator) values (:name, :difficulty, :description, :creator)");
        $stmt->bindParam(':name', $_POST['name'], PDO::PARAM_STR, 255);
        $stmt->bindParam(':difficulty', $_POST['difficulty'], PDO::PARAM_STR, 255);
        $stmt->bindParam(':description', $_POST['description'], PDO::PARAM_STR, 255);
        $stmt->bindParam(':creator', $_POST['creator'], PDO::PARAM_STR, 255);
        $stmt->execute();
        try
        {
            $stmt = $conn->prepare("SELECT * FROM puzzles WHERE name=:name");
            $stmt->bindParam(":name", $_POST['name'], PDO::PARAM_STR);
            $stmt->execute();
            $result = $stmt->fetchAll(PDO::FETCH_ASSOC);

            //echo "Success" . "b" . $result['id'] . "b" . $result['name'] . "b" . $result['difficulty'];
            array_push($result, (object)[
                "status" => "success"
            ]);
    
            echo json_encode($result);

            // echo json_encode(array("Status" => "Success", $result));
        }
        catch(PDOException $e)
        {
            exit($e->getMessage());
        }

    }
}
catch(PDOException $e)
{
    exit($e->getMessage());
}
