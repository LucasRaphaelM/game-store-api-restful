const api = {
    async searchGames() {
        try {
            const response = await fetch('https://localhost:7085/jogo')
            return await response.json()
        }
        catch {
            //alert('Erro ao buscar Jogos')
            throw error
        }
    },
    async searchGamesInAccount(userId) {
        try {
            const response = await fetch(`https://localhost:7085/usuario/${userId}`)
            return await response.json()
        }
        catch {
            //alert('Erro ao buscar Jogos')
            throw error
        }
    },
    async searchGameById(gameId)
    {
        try {
            const response = await fetch(`https://localhost:7085/jogo/${gameId}`)
            return await response.json()
        }
        catch {
            //alert('Erro ao buscar Jogos')
            throw error
        }
    }
}


async function fazerLogin() {
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const statusEl = document.getElementById("status");

    statusEl.innerText = "Enviando...";

    try {
        const res = await fetch("https://localhost:7085/usuario/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Username: username,
                Password: password
            })
        });

        if (!res.ok) {
            const msg = await res.text();
            console.error("Erro do servidor:", msg);
            statusEl.innerText = "Login inválido.";
            return;
        }

        const data = await res.json();
        console.log("Token recebido:", data.token);

        localStorage.setItem("token", data.token);
        statusEl.innerText = "Login bem-sucedido! Redirecionando...";

        setTimeout(() => {
            window.location.href = "index.html";
        }, 1000);

    } catch (err) {
        console.error("Erro na requisição:", err);
        statusEl.innerText = "Erro ao conectar ao servidor.";
    }
}

export function decodeJwt(token) {
    const base64Url = token.split('.')[1]; // pega o payload
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');

    const jsonPayload = decodeURIComponent(
        atob(base64)
            .split('')
            .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
            .join('')
    );

    return JSON.parse(jsonPayload);
}



document.addEventListener("DOMContentLoaded", () => {
    document.getElementById("btnLogin").addEventListener("click", fazerLogin)
})
        
export default api;