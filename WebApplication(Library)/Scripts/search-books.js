$(document).ready(function () {
    const allBooks = $(".book");
    
    $("#nameSearch").on("input", () => {
        $("#book-container").empty();
        for (let book of allBooks) {
            if ($("#nameSearch").val().length != 0) {
                let title = $(book).find("p.title-p span.title-span").text();
                let author = $(book).find("p.author-p span.author-span").text();
                if (title.toLowerCase().includes($("#nameSearch").val().toLowerCase()) || author.toLowerCase().includes($("#nameSearch").val().toLowerCase())) {
                    $("#book-container").append(book);
                }
            }
            else {
                $("#book-container").append(book);
            }
        }
        bindUpdateButtons();
    })
})