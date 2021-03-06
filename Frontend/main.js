import { Kuvar } from "./Kuvar.js";


function createMainScreen() {
//const fetchBegin = ("https://localhost:5500/Frontend/Korisnik")
   const fetchBegin = "https://localhost:5001/"; //putokaz ka svim kontrolerima
   
    let cont = document.createElement("div");
    cont.className = "screen";
    const mainDiv = document.createElement("div");
    mainDiv.className = "naslov";
    cont.appendChild(mainDiv);
    document.body.appendChild(cont);

    let l = document.createElement("h1");
    l.innerHTML = "Dobrodošli, izaberite kuvar";
    mainDiv.appendChild(l);

    let kuvariDiv = document.createElement("div");
    kuvariDiv.className = "odabirKuvara";
    mainDiv.appendChild(kuvariDiv);

    fetch(fetchBegin + "Kuvar/Kuvari", { method: "GET" }) //zovemo kuvare koje imamo za prikaz
        .then(resp => {
            if (resp.ok) {
                resp.json().then(kuvari => {

                    kuvari.forEach(kuvar => {

                        l = document.createElement("img");
                        l.className = "kuvar";
                        l.src = "kuvar" + kuvar.id + ".png";
                        l.addEventListener("click", ev => {
                            document.body.removeChild(cont);
                            var k = new Kuvar(kuvar.id, kuvar.naziv, fetchBegin);
                            k.crtajPocetak(document.body);

                        });
                        kuvariDiv.appendChild(l);
                    });

                });
            }
        });

}

createMainScreen();




