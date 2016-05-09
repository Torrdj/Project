<?php
    $mail = "truong_f@epita.fr";
    $subject = $_POST['name'] . '- Mail: ' . $_POST['email'];
    $body = $_POST['comments'];

    mail($mail, $subject, $body);

    echo '<a href="contact.html"> Message envoyé ! </a>';
?>