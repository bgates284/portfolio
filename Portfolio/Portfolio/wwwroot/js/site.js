// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    document.addEventListener("DOMContentLoaded", function () {
        var audio = document.getElementById("audioPlayer");
        audio.play().catch(error => {
        console.log("Autoplay prevented:", error);
        });
    });
</script>