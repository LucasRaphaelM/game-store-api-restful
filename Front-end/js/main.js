import ui from "./ui.js"

document.addEventListener("DOMContentLoaded", () => {
    ui.showGames()
    ui.searchGamesInAccount()
    ui.showGameById()
})

document.addEventListener("click", (e) => {
    if (e.target && e.target.id === "logout-btn") {
        e.preventDefault(); // evita recarregar a página por ser <a>
        localStorage.clear();
        window.location.href = "login.html";
    }
});

document.addEventListener("click", (e) => {
    const btn = e.target.closest(".button-add-cart-text");
    if (btn) {
        e.preventDefault(); // evita ir imediatamente para a página
        const gameId = btn.dataset.id;

        // Salva o ID do jogo no localStorage
        localStorage.setItem("GameId", gameId);

        // Redireciona para a página do jogo
        window.location.href = "buy.html";
    }
});