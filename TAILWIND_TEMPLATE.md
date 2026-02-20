# Cum folosești un template Tailwind CSS în proiect

## Varianta 1: Tailwind prin CDN (rapid, fără build)

1. Deschide `Views/Shared/_Layout.cshtml`.
2. În `<head>`, **înainte** de celelalte CSS, adaugă:
   ```html
   <script src="https://cdn.tailwindcss.com"></script>
   ```
3. Poți scoate sau păstra Bootstrap; dacă păstrezi ambele, clasele Tailwind (ex: `flex`, `p-4`, `bg-gray-100`) vor funcționa.

După asta poți copia HTML-ul din orice template Tailwind în view-urile Razor și înlocui conținutul static cu Razor (ex: `@Model.Products`, `@p.Name`).

---

## Varianta 2: Template gata făcut (HTML + Tailwind)

1. Descarcă template-ul (ex: de pe [Tailwind UI](https://tailwindui.com), [Flowbite](https://flowbite.com), [DaisyUI](https://daisyui.com), sau [TailwindComponents](https://tailwindcomponents.com)).
2. Deschide fișierul HTML al template-ului și copiază:
   - structura din `<body>` (navbar, main, footer) în `_Layout.cshtml`, sau
   - doar secțiunea de conținut într-un view (ex: `Index.cshtml`, `Category.cshtml`).
3. În `_Layout.cshtml` în `<head>` pune:
   ```html
   <script src="https://cdn.tailwindcss.com"></script>
   ```
   Dacă template-ul folosește plugin-uri (ex: forms, typography), adaugă și configurarea lor după script:
   ```html
   <script src="https://cdn.tailwindcss.com"></script>
   <script>
     tailwind.config = {
       theme: { extend: {} },
       plugins: []  // sau require('@tailwindcss/forms') dacă ai npm
     }
   </script>
   ```
4. Înlocuiește în view-uri textul static cu date din model:
   - ex: o listă de produse → `@foreach (var p in Model.Products) { ... @p.Name ... @p.Price ... }`.

---

## Varianta 3: Tailwind cu build (npm) – pentru producție

1. În rădăcina proiectului (acolo unde e `.csproj`):
   ```bash
   npm init -y
   npm install -D tailwindcss
   npx tailwindcss init
   ```
2. Creează fișier CSS (ex: `wwwroot/css/tailwind-src.css`):
   ```css
   @tailwind base;
   @tailwind components;
   @tailwind utilities;
   ```
3. În `tailwind.config.js` setează:
   ```js
   content: ["./Views/**/*.cshtml", "./wwwroot/**/*.html"]
   ```
4. În `package.json` adaugă script de build:
   ```json
   "scripts": {
     "build:css": "tailwindcss -i ./wwwroot/css/tailwind-src.css -o ./wwwroot/css/tailwind.css --minify"
   }
   ```
   Rulează `npm run build:css` când modifici CSS.
5. În `_Layout.cshtml` înlocuiește CDN-ul cu:
   ```html
   <link rel="stylesheet" href="~/css/tailwind.css" />
   ```

Apoi folosești template-ul la fel: copiezi HTML-ul în view-uri și legi datele cu Razor (`@Model`, `@foreach`, etc.).

---

## Rezumat

- **Doar să încerci un template rapid:** Varianta 1 (CDN) + copiere HTML în view-uri.
- **Template gata făcut:** Varianta 2 (CDN + pașii de copiere și înlocuire cu Razor).
- **Proiect serios / producție:** Varianta 3 (npm + build).

Clasele Tailwind (ex: `container`, `mx-auto`, `grid`, `text-lg`) funcționează în orice view unde ai inclus Tailwind (prin CDN sau prin `tailwind.css`).
