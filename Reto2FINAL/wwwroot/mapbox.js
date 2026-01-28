// Variable global para mantener referencia al mapa actual
let currentMap = null;

window.initializeMap = (token, latitude, longitude, zoom, markerText, mapStyle) => {
    try {
        mapboxgl.accessToken = token;

        // Remover mapa anterior si existe
        if (currentMap) {
            currentMap.remove();
            currentMap = null;
        }

        const container = document.getElementById('map');
        if (!container) {
            console.error("Contenedor 'map' no encontrado");
            return;
        }

        // Limpiar contenedor
        container.innerHTML = '';

        const style = mapStyle || 'mapbox://styles/mapbox/streets-v12';

        // Crear mapa
        currentMap = new mapboxgl.Map({
            container: 'map',
            style: style,
            center: [longitude, latitude],
            zoom: zoom
        });

        // Añadir marcador con popup
        const marker = new mapboxgl.Marker({ color: '#0066cc' })
            .setLngLat([longitude, latitude])
            .setPopup(new mapboxgl.Popup().setHTML(`<strong>${markerText}</strong>`))
            .addTo(currentMap);

        // Controles de navegación y zoom
        currentMap.addControl(new mapboxgl.NavigationControl());

        // Opcional: Direcciones
        // currentMap.addControl(
        //     new MapboxDirections({ accessToken: mapboxgl.accessToken }), 
        //     'top-left'
        // );

        // Mostrar coordenadas al mover el mouse
        currentMap.on('mousemove', function (e) {
            const infoDiv = document.getElementById('info');
            if (infoDiv) {
                infoDiv.innerHTML = `Lat: ${e.lngLat.lat.toFixed(5)}, Lon: ${e.lngLat.lng.toFixed(5)}`;
            }
        });

        console.log("Mapa inicializado correctamente");
    } catch (error) {
        console.error("Error al inicializar el mapa:", error);
    }
};