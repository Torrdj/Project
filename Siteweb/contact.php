<?php
 
if(isset($_POST['email'])) {
 
     
 
    // EDIT THE 2 LINES BELOW AS REQUIRED
 
    $email_to = "truong_f@epita.fr";
 
    $email_subject = $_POST['name'] . ' - Mail: ' . $_POST['email'];
 
     
 
     
 
    function died($error) {
 
        // your error code can go here
 
        echo "Il semble qu'il y ait des erreurs dans le formulaire que vous avez envoyé. <br />";
 
        echo "Celle-ci apparaitront ci-dessous.<br /><br />";
 
        echo $error."<br /><br />";
 
        echo "Merci de bien vouloir les corriger, et de réenvoyer votre formulaire.<br /><br />";
 
        die();
 
    }
 
     
 
    // validation expected data exists
 
    if(!isset($_POST['name']) ||
 
        !isset($_POST['email']) ||
 
        !isset($_POST['comments'])) {
 
        died('Votre formulaire semble invalide.');       
 
    }
 
     
 
    $first_name = $_POST['name']; // required
 
    $email_from = $_POST['email']; // required
 
    $comments = $_POST['comments']; // required
 
     
 
    $error_message = "";
 
    $email_exp = '/^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$/';
 
  if(!preg_match($email_exp,$email_from)) {
 
    $error_message .= 'Votre addresse e-mail semble invalide.<br />';
 
  }
 
    $string_exp = "/^[A-Za-z .'-]+$/";
 
  if(!preg_match($string_exp,$first_name)) {
 
    $error_message .= 'Le nom que vous avez entré semble invalide.<br />';
 
  }
 
  if(strlen($comments) < 10) {
 
    $error_message .= 'Le contenu du message semble trop court, votre message doit faire plus de 10 caractères.<br />';
 
  }
 
  if(strlen($error_message) > 0) {
 
    died($error_message);
 
  }
 
    $email_message = "Form details below.\n\n";
 
     
 
    function clean_string($string) {
 
      $bad = array("content-type","bcc:","to:","cc:","href");
 
      return str_replace($bad,"",$string);
 
    }
 
     
 
    $email_message .= "Nom: ".clean_string($first_name)."\n";
 
    $email_message .= "Email: ".clean_string($email_from)."\n";
 
    $email_message .= "Message: ".clean_string($comments)."\n";
 
     
 
     
 
// create email headers
 
$headers = 'From: '.$email_from."\r\n".
 
'Reply-To: '.$email_from."\r\n" .
 
'X-Mailer: PHP/' . phpversion();
 
@mail($email_to, $email_subject, $email_message, $headers);  
 
?>
 
 
 
<!-- include your own success html here -->

<html>
    <head>
        <meta charset="utf-8" />
        <title>Quantum Quest</title>
        <link rel="stylesheet" href="style.css">
    </head>
    
    <body>
    <div class="header">
        <div class="nav">
            <ul>
                <li><a href="index.html">Accueil</a></li>
                <li><a href="presentation.html">Presentation</a></li>
                <li><a href="personnages.html">Personnages</a></li>
                <li><a href="scriptmakers.html">Script Makers</a></li>
                <li><a href="download.html">Téléchargement</a></li>
                <li><a href="contact.html">Contact</a></li>
                <li><a href="connexion.html">Connexion</a></li>
                <li><a href="inscription.html">Inscription</a></li>
            </ul>
        </div>
    </div>
    <div class="container">
        <div class="text">
            <div class="text_form">
                <p>
                    Merci de nous avoir contacté ! <br />
                    <a href="contact.html"> Retour </a>
                </p>
            </div>
        </div>
    </div>
    </body>
</html>
 
<?php
 
}
 
?>