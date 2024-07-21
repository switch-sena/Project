-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 21-07-2024 a las 02:54:36
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `switch`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ActualizacionUsuarios` (IN `p_Nombre` VARCHAR(100), IN `p_Correo` VARCHAR(100), IN `p_Apellido` VARCHAR(100), IN `p_Genero` SET('Hombre','Mujer','Otro'), IN `p_Fecha_Nacimiento` DATE, IN `p_Celular` VARCHAR(10), IN `p_Correo_Electronico` VARCHAR(100), IN `p_Links_RS` VARCHAR(7000), IN `p_Habilidades` VARCHAR(1000), OUT `p_Registrado` BOOLEAN, OUT `p_Mensaje` VARCHAR(100))   BEGIN

    	UPDATE `usuario` SET `Nombre`=p_Nombre,`Apellido`=p_Apellido,`genero`=p_Genero,`Fecha_Nacimiento`=p_Fecha_Nacimiento,`Celular`=p_Celular,`Correo_Electronico`=p_Correo_Electronico,`Links_RS`=p_Links_RS,`Habilidades`=p_Habilidades
        WHERE `Correo` = p_Correo;
        SET p_Registrado = TRUE;
        SET p_Mensaje = 'Usuario registrado';
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_RegistrarUsuario` (IN `p_Nombre` VARCHAR(100), IN `p_Apellido` VARCHAR(100), IN `p_Correo` VARCHAR(100), IN `p_Clave` VARCHAR(500), OUT `p_Registrado` BOOLEAN, OUT `p_Mensaje` VARCHAR(100), OUT `p_UsuarioExists` INT)   BEGIN
    SELECT COUNT(IdUsuario) INTO p_UsuarioExists FROM usuario WHERE Correo = p_Correo;

    IF p_UsuarioExists = 0 THEN
    	INSERT INTO usuario(Nombre,Apellido,Correo,Clave) VALUES (p_Nombre,p_Apellido,p_Correo,p_Clave);
        SET p_Registrado = TRUE;
        SET p_Mensaje = 'Usuario registrado';
    ELSE
    	SET p_Registrado = FALSE;
        SET p_Mensaje = 'Correo existente';
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ValidarUsuario` (IN `p_Correo` VARCHAR(100), IN `p_Clave` VARCHAR(500), OUT `p_UsuarioExists` INT)   BEGIN
 SELECT COUNT(IdUsuario) INTO p_UsuarioExists FROM usuario WHERE Correo = p_Correo AND Clave = p_Clave;

    IF p_UsuarioExists = 1 THEN
    	SELECT IdUsuario FROM usuario WHERE Correo = p_Correo and Clave = p_Clave;
    ELSE
    	SELECT '0';
     END IF;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `barrios`
--

CREATE TABLE `barrios` (
  `id_barr` int(11) NOT NULL,
  `nombre_barr` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `habilidades`
--

CREATE TABLE `habilidades` (
  `id_habi` int(11) NOT NULL,
  `nombre_habi` varchar(50) NOT NULL,
  `descripcion_habi` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `modalidad`
--

CREATE TABLE `modalidad` (
  `id_moda` int(11) NOT NULL,
  `nombre_moda` varchar(50) NOT NULL,
  `descripcion_moda` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `publicacion`
--

CREATE TABLE `publicacion` (
  `id_publ` int(11) NOT NULL,
  `copia_idusua` int(11) NOT NULL,
  `copia_idmoda` int(11) NOT NULL,
  `titulo_publ` varchar(60) NOT NULL,
  `descripcion_publ` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `publ_habi`
--

CREATE TABLE `publ_habi` (
  `id` int(11) NOT NULL,
  `copia_idpubl` int(11) DEFAULT NULL,
  `copia_idhabi` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id_usua` int(11) NOT NULL,
  `nombre_usua` varchar(100) DEFAULT NULL,
  `apellido_usua` varchar(100) DEFAULT NULL,
  `genero_usua` set('Hombre','Mujer','Otro') DEFAULT NULL,
  `fecha_nacimiento_usua` date DEFAULT NULL,
  `celular_usua` varchar(10) DEFAULT NULL,
  `correo_usua` varchar(100) NOT NULL,
  `clave_usua` varchar(500) NOT NULL,
  `correo_electronico_usua` varchar(100) DEFAULT NULL,
  `links_rs_usua` varchar(7000) DEFAULT NULL,
  `copia_idbarr` int(11) NOT NULL,
  `creacionfecha_usua` timestamp NOT NULL DEFAULT current_timestamp(),
  `modificacionfecha_usua` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `barrios`
--
ALTER TABLE `barrios`
  ADD PRIMARY KEY (`id_barr`);

--
-- Indices de la tabla `habilidades`
--
ALTER TABLE `habilidades`
  ADD PRIMARY KEY (`id_habi`);

--
-- Indices de la tabla `modalidad`
--
ALTER TABLE `modalidad`
  ADD PRIMARY KEY (`id_moda`);

--
-- Indices de la tabla `publicacion`
--
ALTER TABLE `publicacion`
  ADD PRIMARY KEY (`id_publ`),
  ADD KEY `copia_idmoda` (`copia_idmoda`),
  ADD KEY `copia_idusua` (`copia_idusua`);

--
-- Indices de la tabla `publ_habi`
--
ALTER TABLE `publ_habi`
  ADD PRIMARY KEY (`id`),
  ADD KEY `copia_idpubl` (`copia_idpubl`),
  ADD KEY `copia_idhabi` (`copia_idhabi`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id_usua`),
  ADD UNIQUE KEY `correo_usua` (`correo_usua`),
  ADD KEY `fk_usuario_copia_idbarr` (`copia_idbarr`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `barrios`
--
ALTER TABLE `barrios`
  MODIFY `id_barr` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `modalidad`
--
ALTER TABLE `modalidad`
  MODIFY `id_moda` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `publicacion`
--
ALTER TABLE `publicacion`
  MODIFY `id_publ` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `publ_habi`
--
ALTER TABLE `publ_habi`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id_usua` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `publicacion`
--
ALTER TABLE `publicacion`
  ADD CONSTRAINT `publicacion_ibfk_1` FOREIGN KEY (`copia_idmoda`) REFERENCES `modalidad` (`id_moda`),
  ADD CONSTRAINT `publicacion_ibfk_2` FOREIGN KEY (`copia_idusua`) REFERENCES `usuario` (`id_usua`);

--
-- Filtros para la tabla `publ_habi`
--
ALTER TABLE `publ_habi`
  ADD CONSTRAINT `publ_habi_ibfk_1` FOREIGN KEY (`copia_idpubl`) REFERENCES `publicacion` (`id_publ`),
  ADD CONSTRAINT `publ_habi_ibfk_2` FOREIGN KEY (`copia_idhabi`) REFERENCES `habilidades` (`id_habi`);

--
-- Filtros para la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD CONSTRAINT `fk_usuario_copia_idbarr` FOREIGN KEY (`copia_idbarr`) REFERENCES `barrios` (`id_barr`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
