import api from "./api.js"
import { decodeJwt } from "./api.js"

const ui = {
    async showGames(){
        const gameList = document.getElementById('games-cards')
        const userAccount = document.getElementById('usuario-stays-here')

        try{
            const token = localStorage.getItem("token")
            const games = await api.searchGames()
             let username = null;
             let id = null;
             let admin = false;

            if (token)
            {
                const decoded = decodeJwt(token);
                username = decoded.username;
                id = decoded.id;
                admin = decoded.admin;
                userAccount.innerHTML += `
                    <a class="loja-biblioteca" href="library.html">Library</a>
                    <p class="username-header">${username}</p>
                    <a href="#" id="logout-btn">Sair</a>
                `
            }
            else
            {
                userAccount.innerHTML += `
                    <a href="login.html"class="username-header">Login</a>
                `
            }
            games.forEach(game => {
                gameList.innerHTML += `
                    <div class="card-body">
                        <img class="card-body-img" src="${game.foto}">
                        <div class="card-body-infos">
                            <p class="game-card-name"><strong>${game.titulo}</strong></p>
                            <p class="game-card-developer">${game.desenvolvedora}</p>
                            <div class="button-add-cart">
                                <a class="button-add-cart-text" href="buy.html" data-id="${game.id}">
                                    <img class="card-body-cart-img" src="https://www.iconpacks.net/icons/2/free-add-to-cart-icon-3046-thumb.png">
                                R$ 199,90
                                </a>
                            </div>
                        </div>
                    </div>
                `
            });
        }
        catch(error)
        {
            console.error(error);
            //alert('Erro to show games')
        }
    },

    async searchGamesInAccount() {
        const gameListLibrary = document.getElementById('games-cards-library')
        const headerLibrary = document.getElementById('header-section-id')

        try {
            const token = localStorage.getItem("token");
            if (!token) {
                return;
            }

            const decoded = decodeJwt(token);
            const id = decoded.id;

            const gamesLibrary = await api.searchGamesInAccount(id);

            gamesLibrary.conta.pedidos.forEach(pedido => {

                const dataCompra = new Date(pedido.dataCompra);

                const dia = String(dataCompra.getDate()).padStart(2, '0');
                const mes = String(dataCompra.getMonth() + 1).padStart(2, '0');
                const ano = String(dataCompra.getFullYear()).slice(-2);

                const horas = String(dataCompra.getHours()).padStart(2, '0');
                const minutos = String(dataCompra.getMinutes()).padStart(2, '0');
                
                const dataFormatada = `${dia}/${mes}/${ano} às ${horas}:${minutos}`;

                gameListLibrary.innerHTML += `
                    <div class="container-geral-jogo">
                        <div class="glass-background-effect">
                        </div>
                            <img class="card-body-img-bg" src="${pedido.key.jogoPic}">
                        <div class="container-img">
                            <img class="card-body-img" src="${pedido.key.jogoPic}">
                        </div>
                        <div class="card-body-infos">
                            <p class="game-card-name"><strong>${pedido.key.jogoNome}</strong></p>
                            <p class="game-card-developer">${pedido.key.jogoDesenvolvedora}</p>
                            <div class="key-border">
                                <p class="game-card-key">Key: ${pedido.key.keyCode}</p>
                            </div>
                            <p class="game-card-data-compra">Data Da Compra: ${dataFormatada}</p>
                        </div>
                    </div>
                `;
            });
        } catch (error) {
            console.error(error);
            //alert('Erro para games');
        }
    },
    async showGameById()
    {
        const game = document.getElementById('game-card-buy')
        try
        {
            const lS = localStorage.getItem("GameId")
            const gameId = await api.searchGameById(lS)

            game.innerHTML += `
                <div class="container-geral-jogo">
                    <div class="glass-background-effect">
                    </div>
                    <img class="card-body-img-bg" src="${gameId.foto}">
                    <div class="container-img">
                        <img class="card-body-img" src="${gameId.foto}">
                    </div>
                    <div class="card-body-infos">
                        <p class="game-card-name"><strong>${gameId.titulo}</strong></p>
                        <p class="game-card-developer">${gameId.desenvolvedora}</p>
                    </div>
                    <a class="game-buy-button" href="#">R$ ${gameId.preco}</a>
                </div>
            `
        }
        catch
        {

            alert(ls)
        }
    }
}


export default ui;