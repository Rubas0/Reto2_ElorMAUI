using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto02_MAUI.Helpers
{
    public static class MapboxHelper
    {
        private const string MAPBOX_ACCESS_TOKEN = "pk.eyJ1IjoicnViYXNvIiwiYSI6ImNta2VkZXA1dzA1aGczZ3F3NWs2ajVyem8ifQ.yb3UuTdZ4N8ws7bMLuHLHg";

        public static string GenerateMapHtml(double latitude, double longitude, string title, string description)
        {
            // Mapbox usa [longitud, latitud] - INVERTIDO
            return $@"
<! DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no' />
    <title>Mapa - {title}</title>
    
    <!-- Mapbox CDN CSS -->
    <link href='https://api.mapbox.com/mapbox-gl-js/v3.17.0-beta. 1/mapbox-gl.css' rel='stylesheet' />
    
    <!-- Mapbox CDN JS -->
    <script src='https://api.mapbox.com/mapbox-gl-js/v3.17.0-beta.1/mapbox-gl.js'></script>
    
    <style>
        body {{
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
        }}
        #map {{
            position: absolute;
            top:  0;
            bottom: 0;
            width: 100%;
        }}
        .mapboxgl-popup-content {{
            padding: 15px;
            max-width: 300px;
        }}
        .mapboxgl-popup-content h3 {{
            margin:  0 0 10px 0;
            color: #0066CC;
            font-size: 16px;
        }}
        .mapboxgl-popup-content p {{
            margin: 5px 0;
            color: #333;
            font-size: 13px;
        }}
    </style>
</head>
<body>
    <div id='map'></div>
    
    <script>
        // Inicializar Mapbox con el token
        mapboxgl.accessToken = 'pk.eyJ1IjoicnViYXNvIiwiYSI6ImNta2VkZXA1dzA1aGczZ3F3NWs2ajVyem8ifQ.yb3UuTdZ4N8ws7bMLuHLHg';
        
        // Crear el mapa
        var map = new mapboxgl.Map({{
            container: 'map',
            style: 'mapbox://styles/mapbox/streets-v12',
            center: [{longitude}, {latitude}],  // [lon, lat]
            zoom: 15
        }});

        // Añadir controles de navegación (zoom, rotación)
        map.addControl(new mapboxgl.NavigationControl());

        // Crear popup con información del centro
        var popup = new mapboxgl.Popup({{ 
            offset: 25,
            closeButton: true,
            closeOnClick: false
        }})
        .setHTML('<h3>{title}</h3><p>{description}</p>');

        // Crear marcador personalizado (color naranja corporativo)
        var marker = new mapboxgl.Marker({{
            color: '#FF6B35'
        }})
        .setLngLat([{longitude}, {latitude}])
        .setPopup(popup)
        .addTo(map);

        // Mostrar popup automáticamente al cargar
        setTimeout(function() {{
            marker.togglePopup();
        }}, 500);
    </script>
</body>
</html>";
        }

        public static bool IsTokenConfigured()
        {
            return !string.IsNullOrEmpty(MAPBOX_ACCESS_TOKEN) && MAPBOX_ACCESS_TOKEN != "pk.eyJ1IjoicnViYXNvIiwiYSI6ImNta2VkZXA1dzA1aGczZ3F3NWs2ajVyem8ifQ.yb3UuTdZ4N8ws7bMLuHLHg";
        }

        public static string GetPlaceholderHtml(string ubicacion, string coordenadas)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <style>
        body {{
            margin: 0;
            padding:  20px;
            font-family:  Arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #f5f5f5;
        }}
        .placeholder {{
            text-align: center;
            padding: 40px;
            background:  white;
            border-radius:  10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }}
        h2 {{ 
            color: #FF6B35; 
            margin-bottom: 20px;
        }}
        p {{ 
            color: #666; 
            margin:  10px 0;
        }}
        .info {{
            background: #f0f0f0;
            padding: 15px;
            border-radius:  5px;
            margin-top: 20px;
        }}
    </style>
</head>
<body>
    <div class='placeholder'>
        <h2>Mapa no disponible</h2>
        <p>Configura tu token de Mapbox en MapboxHelper.cs</p>
        <div class='info'>
            <p><strong>Ubicación:</strong> {ubicacion}</p>
            <p><strong>Coordenadas:</strong> {coordenadas}</p>
        </div>
        <p style='margin-top: 20px; font-size: 12px;'>
            Obtén tu token gratuito en: <br/>
            <a href='https://account.mapbox.com/' target='_blank'>https://account.mapbox.com/</a>
        </p>
    </div>
</body>
</html>";
        }
    }
}