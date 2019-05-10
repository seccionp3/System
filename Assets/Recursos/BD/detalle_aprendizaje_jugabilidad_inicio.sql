select  
color.r_color, color.g_color, color.b_color, ubicacion.nombre_ubicacion, tpersonaje.audio_personaje, tpersonaje.imagen_personaje
from detalle_aprendizaje  as detalle_aprendizaje
inner join color
on color.id = id_color
inner join ubicacion
on ubicacion.id = id_ubicacion
inner join personaje as tpersonaje
on tpersonaje.id_personaje = detalle_aprendizaje.id_personaje
where id_detalle_apre = 28;