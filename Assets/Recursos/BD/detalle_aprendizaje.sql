CREATE TABLE `detalle_aprendizaje_acierto` (
	`id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`tiempo_reaccion`	TEXT,
	`tiempo_cuadro`	TEXT,
	`tiempo_boton`	TEXT,
	`acierto`	INTEGER,
	`id_detalle_aprendizaje` INTEGER,
	foreign key (id_detalle_aprendizaje) references personaje (id_detalle_apre)
);