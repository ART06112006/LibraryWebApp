function bindUpdateButtons() {
    $(".updateBookButton").off('click').on('click', function () {
        let card = $(this).closest('.book-card');

        let id = card.find("p.bookId").text();
        $("#updateBookForm").find("#bookId").val(id);

        let photo = card.find("img.book-cover").attr("src");
        $("#updateBookForm").find("#photo").val(photo);

        let title = card.find("p.bookTitle").text().replace("Title:", "").trim();
        $("#updateBookForm").find("#title").val(title);

        let author = card.find("p.bookAuthor").text().replace("Author:", "").trim();
        $("#updateBookForm").find("#author").val(author);

        let year = card.find("p.bookYear").text().replace("Year published:", "").trim();
        $("#updateBookForm").find("#year").val(year);
    });
} $(document).ready(function () {
    bindUpdateButtons();
});
