-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 09-12-2025 a las 20:09:22
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `appduocofcourse`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `answers`
--

CREATE TABLE `answers` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `attempt_id` bigint(20) UNSIGNED NOT NULL,
  `question_id` bigint(20) UNSIGNED NOT NULL,
  `option_id` bigint(20) UNSIGNED DEFAULT NULL,
  `open_text` text DEFAULT NULL,
  `is_correct` tinyint(1) DEFAULT NULL,
  `points_awarded` decimal(6,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `answers`
--

INSERT INTO `answers` (`id`, `attempt_id`, `question_id`, `option_id`, `open_text`, `is_correct`, `points_awarded`) VALUES
(33, 38, 25, 59, NULL, 1, 1.00),
(34, 39, 26, 62, NULL, 0, 0.00),
(35, 40, 18, 43, NULL, NULL, NULL),
(36, 40, 19, 47, NULL, NULL, NULL),
(37, 41, 27, 63, NULL, NULL, NULL),
(38, 41, 28, 65, NULL, NULL, NULL),
(39, 42, 29, 68, NULL, NULL, NULL),
(40, 42, 30, 70, NULL, NULL, NULL),
(41, 42, 31, 71, NULL, NULL, NULL),
(42, 43, 32, 73, NULL, NULL, NULL),
(43, 43, 33, 75, NULL, NULL, NULL),
(44, 44, 34, 77, NULL, 1, 1.00),
(45, 44, 35, 79, NULL, 1, 1.00),
(46, 44, 36, 81, NULL, 1, 1.00),
(47, 45, 37, 84, NULL, NULL, NULL),
(48, 46, 39, 87, NULL, 1, 1.00),
(49, 47, 38, 85, NULL, NULL, NULL),
(50, 48, 27, 63, NULL, NULL, NULL),
(51, 48, 28, 65, NULL, NULL, NULL),
(52, 49, 29, 67, NULL, NULL, NULL),
(53, 49, 30, 70, NULL, NULL, NULL),
(54, 49, 31, 71, NULL, NULL, NULL),
(55, 50, 32, 74, NULL, NULL, NULL),
(56, 50, 33, 75, NULL, NULL, NULL),
(57, 51, 37, 84, NULL, NULL, NULL),
(58, 52, 16, 38, NULL, NULL, NULL),
(59, 52, 17, 40, NULL, NULL, NULL),
(60, 53, 23, 56, NULL, NULL, NULL),
(61, 55, 40, 89, NULL, NULL, NULL),
(62, 56, 41, 91, NULL, 1, 1.00),
(63, 57, 40, 89, NULL, NULL, NULL),
(64, 62, 41, 91, NULL, 1, 1.00),
(65, 63, 41, 92, NULL, 0, 0.00),
(66, 64, 25, 59, NULL, 1, 1.00),
(67, 65, 34, 77, NULL, 1, 1.00),
(68, 65, 35, 79, NULL, 1, 1.00),
(69, 65, 36, 82, NULL, 0, 0.00),
(70, 66, 41, 91, NULL, 1, 1.00);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `attempts`
--

CREATE TABLE `attempts` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `evaluation_id` bigint(20) UNSIGNED NOT NULL,
  `student_id` bigint(20) UNSIGNED NOT NULL,
  `started_at` datetime NOT NULL DEFAULT current_timestamp(),
  `submitted_at` datetime DEFAULT NULL,
  `score` decimal(8,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `attempts`
--

INSERT INTO `attempts` (`id`, `evaluation_id`, `student_id`, `started_at`, `submitted_at`, `score`) VALUES
(30, 26, 27, '2025-12-08 20:06:54', '2025-12-08 20:06:54', 100.00),
(31, 32, 27, '2025-12-08 20:07:04', '2025-12-08 20:07:04', 100.00),
(32, 33, 27, '2025-12-08 22:55:31', NULL, NULL),
(33, 34, 27, '2025-12-08 22:56:26', NULL, NULL),
(34, 33, 27, '2025-12-08 23:10:19', NULL, NULL),
(35, 34, 27, '2025-12-08 23:10:22', NULL, NULL),
(36, 33, 27, '2025-12-08 23:10:26', NULL, NULL),
(37, 34, 27, '2025-12-08 23:10:29', NULL, NULL),
(38, 33, 27, '2025-12-08 23:10:36', '2025-12-08 23:10:39', 1.00),
(39, 34, 27, '2025-12-08 23:10:48', '2025-12-08 23:10:52', 0.00),
(40, 28, 27, '2025-12-08 23:18:55', '2025-12-08 23:18:55', 50.00),
(41, 35, 27, '2025-12-08 23:35:06', '2025-12-08 23:35:06', 100.00),
(42, 37, 27, '2025-12-08 23:35:15', '2025-12-08 23:35:15', 100.00),
(43, 40, 27, '2025-12-08 23:35:20', '2025-12-08 23:35:20', 50.00),
(44, 42, 27, '2025-12-08 23:36:33', '2025-12-08 23:36:37', 3.00),
(45, 43, 27, '2025-12-08 23:42:31', '2025-12-08 23:42:31', 100.00),
(46, 45, 27, '2025-12-09 06:09:18', '2025-12-09 06:09:24', 1.00),
(47, 44, 27, '2025-12-09 06:09:31', '2025-12-09 06:09:31', 100.00),
(48, 35, 101, '2025-12-09 15:56:06', '2025-12-09 15:56:06', 100.00),
(49, 37, 101, '2025-12-09 15:56:15', '2025-12-09 15:56:15', 66.67),
(50, 40, 101, '2025-12-09 15:56:24', '2025-12-09 15:56:24', 100.00),
(51, 43, 101, '2025-12-09 15:56:32', '2025-12-09 15:56:32', 100.00),
(52, 26, 101, '2025-12-09 15:58:16', '2025-12-09 15:58:16', 100.00),
(53, 32, 101, '2025-12-09 15:58:22', '2025-12-09 15:58:22', 0.00),
(54, 47, 101, '2025-12-09 16:05:57', NULL, NULL),
(55, 46, 101, '2025-12-09 16:06:03', '2025-12-09 16:06:03', 100.00),
(56, 47, 101, '2025-12-09 16:06:06', '2025-12-09 16:06:08', 1.00),
(57, 46, 27, '2025-12-09 16:06:55', '2025-12-09 16:06:55', 100.00),
(58, 47, 101, '2025-12-09 17:39:00', NULL, NULL),
(59, 47, 101, '2025-12-09 17:39:03', NULL, NULL),
(60, 47, 101, '2025-12-09 17:39:39', NULL, NULL),
(61, 47, 101, '2025-12-09 17:39:45', NULL, NULL),
(62, 47, 101, '2025-12-09 17:39:46', '2025-12-09 17:39:48', 1.00),
(63, 47, 101, '2025-12-09 17:39:52', '2025-12-09 17:39:54', 0.00),
(64, 33, 101, '2025-12-09 17:41:06', '2025-12-09 17:41:08', 1.00),
(65, 42, 101, '2025-12-09 17:41:24', '2025-12-09 17:41:28', 2.00),
(66, 47, 27, '2025-12-09 17:51:57', '2025-12-09 17:52:00', 1.00);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `auth_credentials`
--

CREATE TABLE `auth_credentials` (
  `user_id` bigint(20) UNSIGNED NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `email_verified` tinyint(1) NOT NULL DEFAULT 0,
  `last_login_at` datetime DEFAULT NULL,
  `password_updated_at` datetime NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `auth_credentials`
--

INSERT INTO `auth_credentials` (`user_id`, `password_hash`, `email_verified`, `last_login_at`, `password_updated_at`, `created_at`, `updated_at`) VALUES
(15, '$2a$11$FMONjwYnLhWdIBs.bpzXM.lOEgFGA7jzlM6XKBLoiNk1Lcww5LAiW', 0, NULL, '2025-11-23 15:16:50', '2025-11-23 15:16:50', '2025-11-23 15:16:50'),
(16, '$2a$11$ufBHM3G6P5YHtSeWsyH9Z.D/w9vze634uwa07soFBaMu3YAghI0k2', 0, NULL, '2025-11-23 16:57:27', '2025-11-23 16:57:27', '2025-11-23 16:57:27'),
(23, '$2a$11$q0kBpowL8AaEQLTczBleb.mIMp/3pkJYirewPgLVtwAvdgsofqtjG', 0, NULL, '2025-11-27 22:00:28', '2025-11-27 22:00:28', '2025-11-27 19:01:28'),
(24, '$2a$11$jkJo4lwDadmQQJZM1OzcmuSDqwrvVOLRlVyKCRf829vn5xl7vK9pO', 0, NULL, '0000-00-00 00:00:00', '2025-11-27 19:02:58', '2025-11-27 19:04:29'),
(25, '$2a$11$wHl8XIadYzszFc3eNT7oCeTw9107zFy6B4pQ7j/U3qaS.7ByIBMPS', 0, NULL, '2025-11-27 22:46:36', '2025-11-27 22:46:36', '2025-11-27 22:46:36'),
(26, '$2a$11$qJU9A1q1mWBooAQ8ObZIt.mkYn/ktAVp6Ic7m1z3uAe40lLcEjT1.', 0, NULL, '2025-11-27 23:10:18', '2025-11-27 23:10:18', '2025-11-27 23:10:18'),
(27, '$2a$11$gydnM7reCA3Pc0fVdXbuxO4epx7OBX4HwPiihMYhRhcdUh9bfH.Vq', 0, NULL, '2025-11-27 20:17:45', '2025-11-27 23:17:04', '2025-11-27 20:17:45'),
(101, '$2a$11$mafTUi8tfISH5Jvr/tMYZOGAey9oNiQJYiGph/1FDjwcpZQLg/gxm', 0, NULL, '2025-12-09 13:13:45', '2025-12-09 15:50:54', '2025-12-09 13:13:45');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `certificates`
--

CREATE TABLE `certificates` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `enrollment_id` bigint(20) UNSIGNED NOT NULL,
  `pdf_path` varchar(500) NOT NULL,
  `issued_at` datetime NOT NULL DEFAULT current_timestamp(),
  `verification_code` char(16) NOT NULL,
  `grade_summary` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`grade_summary`))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `courses`
--

CREATE TABLE `courses` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `title` varchar(160) NOT NULL,
  `description` text DEFAULT NULL,
  `cover_image_path` varchar(255) DEFAULT NULL,
  `teacher_id` bigint(20) UNSIGNED NOT NULL,
  `category_id` bigint(20) UNSIGNED NOT NULL,
  `school_id` bigint(20) UNSIGNED NOT NULL,
  `starts_at` datetime DEFAULT NULL,
  `ends_at` datetime DEFAULT NULL,
  `is_published` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `courses`
--

INSERT INTO `courses` (`id`, `title`, `description`, `cover_image_path`, `teacher_id`, `category_id`, `school_id`, `starts_at`, `ends_at`, `is_published`, `created_at`, `updated_at`) VALUES
(11, 'Mecánica Básica', 'Mecánica básica', NULL, 23, 66, 7, NULL, NULL, 0, '2025-11-27 22:07:15', NULL),
(12, 'Microfonía', 'Microfonía', NULL, 23, 3, 2, NULL, NULL, 0, '2025-11-27 23:11:21', NULL),
(14, 'Hola', 'dasdasdsa', NULL, 23, 1, 1, NULL, NULL, 0, '2025-12-08 16:11:09', NULL),
(15, 'Test', 's', NULL, 23, 2, 1, NULL, NULL, 0, '2025-12-09 06:08:25', NULL),
(16, 'Test 2', 'asdasd', NULL, 23, 1, 4, NULL, NULL, 0, '2025-12-09 16:05:02', NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `course_categories`
--

CREATE TABLE `course_categories` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `school_id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(120) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `course_categories`
--

INSERT INTO `course_categories` (`id`, `school_id`, `name`) VALUES
(1, 1, 'Administración de empresas'),
(2, 1, 'Administración pública'),
(3, 1, 'Auditoría'),
(4, 1, 'Auditoría y análisis de negocios'),
(5, 1, 'Comercio exterior'),
(6, 1, 'Contabilidad general mención legislación tributaria'),
(7, 1, 'Contabilidad tributaria'),
(8, 1, 'Ingeniería en administración mención finanzas'),
(9, 2, 'Animación Digital'),
(10, 2, 'Comunicación Audiovisual'),
(11, 2, 'Ingeniería en Sonido'),
(12, 2, 'Publicidad'),
(13, 2, 'Relaciones Públicas y Comunicación Organizacional'),
(14, 2, 'Técnico Audiovisual'),
(15, 2, 'Técnico en Trabajo Social'),
(16, 2, 'Tecnología en Sonido e Iluminación'),
(25, 3, 'Dibujo y modelamiento arquitectónico y estructural'),
(26, 3, 'Ingeniería en construcción'),
(27, 3, 'Ingeniería en prevención de riesgos'),
(28, 3, 'Restauración de bienes patrimoniales'),
(29, 3, 'Técnico en construcción'),
(30, 3, 'Técnico en prevención de riesgos'),
(31, 3, 'Técnico en prevención de riesgos laborales'),
(32, 3, 'Técnico topógrafo geomático'),
(33, 4, 'Desarrollo y diseño web'),
(34, 4, 'Diseño de ambientes'),
(35, 4, 'Diseño de vestuario'),
(36, 4, 'Diseño gráfico'),
(37, 4, 'Diseño industrial e innovación en productos'),
(38, 4, 'Ilustración para contextos globales'),
(50, 5, 'Gastronomía'),
(51, 5, 'Gastronomía Internacional'),
(52, 6, 'Analista Programador'),
(53, 6, 'Analista Programador Computacional'),
(54, 6, 'Desarrollo de Aplicaciones'),
(55, 6, 'Ingeniería en Ciberseguridad'),
(56, 6, 'Ingeniería en Ciencia de Datos'),
(57, 6, 'Ingeniería en Desarrollo de Software'),
(58, 6, 'Ingeniería en Informática'),
(59, 6, 'Ingeniería en Infraestructura Tecnológica'),
(60, 6, 'Ingeniería en Redes y Telecomunicaciones'),
(61, 7, 'Ingeniería Agrícola'),
(62, 7, 'Ingeniería en Automatización y Control Industrial'),
(63, 7, 'Ingeniería en Electricidad y Automatización Industrial'),
(64, 7, 'Ingeniería en Mantenimiento Industrial'),
(65, 7, 'Ingeniería en Maquinaria y Vehículos Pesados'),
(66, 7, 'Ingeniería en Mecánica Automotriz y Autotrónica'),
(67, 7, 'Ingeniería en Medio Ambiente'),
(68, 7, 'Ingeniería Industrial'),
(69, 8, 'Informática Biomédica'),
(70, 8, 'Preparador Físico'),
(73, 8, 'Técnico de Enfermería'),
(71, 8, 'Técnico de Laboratorio Clínico y Banco de Sangre'),
(72, 8, 'Técnico de Radiodiagnóstico y Radioterapia'),
(74, 8, 'Técnico en Odontología'),
(75, 8, 'Técnico en Química y Farmacia'),
(76, 9, 'Administración en Turismo y Hospitalidad'),
(77, 9, 'Técnico en Turismo y Hospitalidad');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `course_modules`
--

CREATE TABLE `course_modules` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `course_id` bigint(20) UNSIGNED NOT NULL,
  `title` varchar(160) NOT NULL,
  `position` int(10) UNSIGNED NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `course_modules`
--

INSERT INTO `course_modules` (`id`, `course_id`, `title`, `position`) VALUES
(7, 11, 'Módulo 1', 1),
(8, 12, 'Módulo 1', 1),
(9, 14, 'Módulo 1', 1),
(10, 15, 'Módulo 1', 1),
(11, 16, 'Módulo 1', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `course_progress`
--

CREATE TABLE `course_progress` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `enrollment_id` bigint(20) UNSIGNED NOT NULL,
  `percentage` decimal(5,2) NOT NULL DEFAULT 0.00,
  `last_update_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `enrollments`
--

CREATE TABLE `enrollments` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `student_id` bigint(20) UNSIGNED NOT NULL,
  `course_id` bigint(20) UNSIGNED NOT NULL,
  `enrolled_at` datetime NOT NULL DEFAULT current_timestamp(),
  `status` enum('IN_PROGRESS','COMPLETED','WITHDRAWN') NOT NULL DEFAULT 'IN_PROGRESS'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `enrollments`
--

INSERT INTO `enrollments` (`id`, `student_id`, `course_id`, `enrolled_at`, `status`) VALUES
(10, 25, 11, '2025-11-27 22:47:08', 'IN_PROGRESS'),
(11, 26, 12, '2025-11-27 23:13:51', 'IN_PROGRESS'),
(12, 27, 11, '2025-12-08 18:08:13', 'IN_PROGRESS'),
(13, 27, 12, '2025-12-08 22:56:22', 'IN_PROGRESS'),
(14, 27, 14, '2025-12-08 23:33:39', 'IN_PROGRESS'),
(15, 27, 15, '2025-12-09 06:09:00', 'IN_PROGRESS'),
(16, 101, 14, '2025-12-09 15:55:30', 'IN_PROGRESS'),
(17, 101, 11, '2025-12-09 15:57:41', 'IN_PROGRESS'),
(18, 101, 16, '2025-12-09 16:05:51', 'IN_PROGRESS'),
(19, 27, 16, '2025-12-09 16:06:50', 'IN_PROGRESS');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `evaluations`
--

CREATE TABLE `evaluations` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `course_id` bigint(20) UNSIGNED NOT NULL,
  `lesson_id` bigint(20) UNSIGNED DEFAULT NULL,
  `title` varchar(160) NOT NULL,
  `description` text DEFAULT NULL,
  `due_at` datetime DEFAULT NULL,
  `type` enum('QUIZ','TASK','EXAM') NOT NULL DEFAULT 'QUIZ',
  `pass_threshold` decimal(5,4) NOT NULL DEFAULT 0.6000,
  `is_final_exam` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT NULL,
  `is_published` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `evaluations`
--

INSERT INTO `evaluations` (`id`, `course_id`, `lesson_id`, `title`, `description`, `due_at`, `type`, `pass_threshold`, `is_final_exam`, `created_at`, `updated_at`, `is_published`) VALUES
(26, 11, 7, '¿Cuál es la herramienta principal para desatornillar?', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-11-27 22:52:50', NULL, 0),
(27, 11, 7, '¿Cuál es la herramienta principal para martillar?', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-11-27 22:53:15', NULL, 0),
(28, 12, 9, '¿Qué micrófono es recomendado en este caso?', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-11-27 23:12:58', NULL, 0),
(29, 12, 9, '¿Qué micrófono no es recomendado en este caso?', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-11-27 23:13:10', NULL, 0),
(32, 11, 8, 'sdads', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-08 18:53:55', NULL, 0),
(33, 11, NULL, 'Evaluación mecánica básica', 'Evaluación mecánica básica', NULL, 'EXAM', 0.6000, 1, '2025-12-08 22:55:12', NULL, 0),
(34, 12, NULL, 'sdadasda', 'dassdadadad', NULL, 'EXAM', 0.6000, 1, '2025-12-08 22:56:09', NULL, 0),
(35, 14, 10, 'hola', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-08 23:34:16', NULL, 0),
(36, 14, 10, 'hola', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-08 23:34:20', NULL, 0),
(37, 14, 11, 'hola', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-08 23:34:28', NULL, 0),
(38, 14, 11, 'hola', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-08 23:34:31', NULL, 0),
(39, 14, 11, 'hola', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-08 23:34:33', NULL, 0),
(40, 14, 12, 'hola', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-08 23:34:45', NULL, 0),
(41, 14, 12, 'hola', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-08 23:34:48', NULL, 0),
(42, 14, NULL, 'Hola', 'hola', NULL, '', 0.6000, 1, '2025-12-08 23:36:10', NULL, 0),
(43, 14, 13, 'a', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-08 23:42:19', NULL, 0),
(44, 15, 14, 'a', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-09 06:08:39', NULL, 0),
(45, 15, NULL, 'aaa', 'aaa', NULL, '', 0.6000, 1, '2025-12-09 06:08:47', NULL, 0),
(46, 16, 15, 'dasdasda 1', NULL, NULL, 'QUIZ', 0.6000, 0, '2025-12-09 16:05:19', NULL, 0),
(47, 16, NULL, 'tewst 2', 'asdsa', NULL, '', 0.6000, 1, '2025-12-09 16:05:30', NULL, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `lessons`
--

CREATE TABLE `lessons` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `module_id` bigint(20) UNSIGNED NOT NULL,
  `title` varchar(160) NOT NULL,
  `content_url` varchar(500) DEFAULT NULL,
  `duration_minutes` int(10) UNSIGNED DEFAULT NULL,
  `position` int(10) UNSIGNED NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `lessons`
--

INSERT INTO `lessons` (`id`, `module_id`, `title`, `content_url`, `duration_minutes`, `position`) VALUES
(7, 7, 'Clase 1', 'https://localhost:7037/media/courses/11/lessons/7/caae2952-9ae4-42ad-a0f1-a50afd8ea7e5.mp4', NULL, 1),
(8, 7, 'Clase 2', NULL, NULL, 2),
(9, 8, 'Clase 1', 'https://localhost:7037/media/courses/12/lessons/9/31a1f39b-cd82-4906-83a3-c61b89844290.mp4', NULL, 1),
(10, 9, 'Clase 1', NULL, NULL, 1),
(11, 9, 'Clase 2', NULL, NULL, 2),
(12, 9, 'Clase 3', NULL, NULL, 3),
(13, 9, 'Clase 4', NULL, NULL, 4),
(14, 10, 'aaa', NULL, NULL, 1),
(15, 11, 'dasda', NULL, NULL, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `lesson_progress`
--

CREATE TABLE `lesson_progress` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `enrollment_id` bigint(20) UNSIGNED NOT NULL,
  `lesson_id` bigint(20) UNSIGNED NOT NULL,
  `completed_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `media`
--

CREATE TABLE `media` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `course_id` bigint(20) UNSIGNED NOT NULL,
  `title` varchar(200) NOT NULL,
  `mime_type` varchar(120) NOT NULL,
  `storage_path` varchar(500) NOT NULL,
  `size_bytes` bigint(20) UNSIGNED DEFAULT NULL,
  `checksum_sha256` char(64) DEFAULT NULL,
  `uploaded_at` datetime NOT NULL DEFAULT current_timestamp(),
  `uploaded_by` bigint(20) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `media`
--

INSERT INTO `media` (`id`, `course_id`, `title`, `mime_type`, `storage_path`, `size_bytes`, `checksum_sha256`, `uploaded_at`, `uploaded_by`) VALUES
(7, 11, 'Analiza el siguiente video, luego responde las preguntas', 'video/mp4', 'https://localhost:7037/media/courses/11/lessons/7/caae2952-9ae4-42ad-a0f1-a50afd8ea7e5.mp4', 6752735, '', '2025-11-27 19:51:49', 23),
(8, 12, 'Microfonía', 'video/mp4', 'https://localhost:7037/media/courses/12/lessons/9/31a1f39b-cd82-4906-83a3-c61b89844290.mp4', 6752735, '', '2025-11-27 20:12:02', 23);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `notifications`
--

CREATE TABLE `notifications` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `user_id` bigint(20) UNSIGNED NOT NULL,
  `kind` enum('NEW_COURSE','EXAM','REMINDER','CERT_AVAILABLE') NOT NULL,
  `payload` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`payload`)),
  `channel` enum('EMAIL') NOT NULL DEFAULT 'EMAIL',
  `status` enum('PENDING','SENT','ERROR') NOT NULL DEFAULT 'PENDING',
  `scheduled_at` datetime DEFAULT NULL,
  `sent_at` datetime DEFAULT NULL,
  `error_msg` varchar(500) DEFAULT NULL,
  `created_at` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `options`
--

CREATE TABLE `options` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `question_id` bigint(20) UNSIGNED NOT NULL,
  `text` text NOT NULL,
  `is_correct` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `options`
--

INSERT INTO `options` (`id`, `question_id`, `text`, `is_correct`) VALUES
(37, 16, 'Taladro', 0),
(38, 16, 'Destornillador', 1),
(39, 16, 'Martillo', 0),
(40, 17, 'Martillo', 1),
(41, 17, 'Destornillador', 0),
(42, 17, 'Taladro', 0),
(43, 18, 'Dinámico', 1),
(44, 18, 'Condensador', 0),
(45, 18, 'Lápiz', 0),
(46, 19, 'Dinámico', 0),
(47, 19, 'Condensador', 0),
(48, 19, 'Lápiz', 1),
(55, 23, 'dasdadadad', 1),
(56, 23, 'dasdad', 0),
(59, 25, 'dasdad', 1),
(60, 25, 'asdasdadasdasd', 0),
(61, 26, 'aaa', 1),
(62, 26, 'bbbb', 0),
(63, 27, 'hola', 1),
(64, 27, 'adios', 0),
(65, 28, 'hola', 1),
(66, 28, 'adios', 0),
(67, 29, 'adios', 0),
(68, 29, 'hola', 1),
(69, 30, 'adios', 0),
(70, 30, 'hola', 1),
(71, 31, 'hola', 1),
(72, 31, 'adios', 0),
(73, 32, 'adios', 0),
(74, 32, 'hola', 1),
(75, 33, 'hola', 1),
(76, 33, 'adios', 0),
(77, 34, 'hola', 1),
(78, 34, 'adios', 0),
(79, 35, 'hola', 1),
(80, 35, 'adios', 0),
(81, 36, 'hola', 1),
(82, 36, 'adios', 0),
(83, 37, 'b', 0),
(84, 37, 'a', 1),
(85, 38, 'a', 1),
(86, 38, 'b', 0),
(87, 39, 'a', 1),
(88, 39, 'v', 0),
(89, 40, '1', 1),
(90, 40, '2', 0),
(91, 41, 'a', 1),
(92, 41, 'd', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `password_reset_tokens`
--

CREATE TABLE `password_reset_tokens` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `user_id` bigint(20) UNSIGNED NOT NULL,
  `token` varchar(200) NOT NULL,
  `expires_at` datetime NOT NULL,
  `is_used` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `password_reset_tokens`
--

INSERT INTO `password_reset_tokens` (`id`, `user_id`, `token`, `expires_at`, `is_used`) VALUES
(16, 27, 'JNDswUTyc70956f7UZuf2lIggukPe_uC0b513YZhi3mijkss7gqIrvvqesLkYyCVddBdPqZWXb-R_oAGAtgpPw', '2025-11-28 00:17:18', 1),
(17, 27, 'i0l5LlfvPFeZp9H6brlYYiba9dKruwrVWsoS02KPN7NM3w7fLekqYILyl5z9tPh1CghUE5JfJxJGig3YE_gB_Q', '2025-11-28 00:21:16', 0),
(18, 101, 'TGhoQ7HmNFsS1E9pxwxXxfDr7wu3KdEpSEJJqsAcAOEM9ysVGQ9VFa89CIrF6m8FwqroeHvDXbOGl2RzE18a8g', '2025-12-09 17:11:50', 1),
(19, 101, 'D8ADVdGZYAXTu6H953mGMaXm3peS1ZQenVlA9Vd3OHKX7RhNA7s3bLOjM2Kv0JObk8uE_H5Q8wXDt0fOMk45xg', '2025-12-09 17:12:25', 1),
(20, 101, 'b_kAyqUQGWLxBmLKyavyO3PubmbyH-YCgYSdXX1lWVjGmuAVTfEdcA-Y1IlWU0Fvr7fBLw0YhAkm7RkJeVgKlg', '2025-12-09 17:12:27', 1),
(21, 101, 'D49PVeXkJdGiEsBxYKZGkQXDMO3BIAcwavz-yc_GH7QhWU6Cw8NsiD6OQIWNygzv3zduCQwR-JQb1O0kXdvoyw', '2025-12-09 17:12:27', 1),
(22, 101, 'lB8RjIx_KZF2v4CZk1RLbHXULKJObvax69cN5KUSpp-tJYCjK3Yyq6CgzWWuOuYBiaq5jSt3wvTeBgp6-5uB0A', '2025-12-09 17:12:28', 1),
(23, 101, 'y9P9ezcvtiPS6GnwGNmkNH96KxIbErUjvyjOmxqYZYHFjFIYaVlseTkvp1hvmcvClhyCLIJI9gMD4CqiFQ74Tw', '2025-12-09 17:12:29', 1),
(24, 101, 'Bua3qAOMyePdPaswJE_RkNdtatJXyukYhtIZYjEW3dD1TTak_3nyI_4MbjmYaYmGMHs7zGPDNUSshnueNU7lug', '2025-12-09 17:12:30', 1),
(25, 101, '9SRLE2GaAyiVzZQPiOUPRGDpRGT7r1GFvgy7ifHkOe75Ud1Aa8i94klqNgzHPJiTipou6NMmQ0-Yq143Xa70gg', '2025-12-09 17:12:40', 1),
(26, 101, 'wdbRX_8UdvYXITb5l6fJHPKNiSVTY_NOhT3mRCQBBmqs7-IwOmR5aQg1c5bVNBrjIkMdbWpccGDiF7FVaxZtRw', '2025-12-09 17:12:45', 1),
(27, 101, 'YpUvTAzzU5DiOI_oubK7H5w9coPZHFVzJeX870h0E4N-w3IzDrEUWvoTd_-2Vtt0lRji8V7rp8Mm7mqVVudUWA', '2025-12-09 17:12:47', 1),
(28, 101, 'HhmkiWB1D0F6aWYUQNmkYDUmTjrvCZxgiOr4MCIFaZBlBFYerttuLxMjvEABMrSA26RekLTPnqiQ_NLR3sJoMg', '2025-12-09 17:12:47', 1),
(29, 101, 'j2cwTE3Vm0v2SSKevKeWEo-YPHXBDGbyefHu0yG6PfcFaq0F3-W3kHVDRnhjDmMKliV-CpLBInf_Amm5VRJbsg', '2025-12-09 17:12:48', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `questions`
--

CREATE TABLE `questions` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `evaluation_id` bigint(20) UNSIGNED NOT NULL,
  `prompt` text NOT NULL,
  `type` enum('SINGLE','MULTI','OPEN') NOT NULL DEFAULT 'SINGLE',
  `points` decimal(6,2) NOT NULL DEFAULT 1.00,
  `position` int(10) UNSIGNED NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `questions`
--

INSERT INTO `questions` (`id`, `evaluation_id`, `prompt`, `type`, `points`, `position`) VALUES
(16, 26, '¿Cuál es la herramienta principal para desatornillar?', 'SINGLE', 1.00, 1),
(17, 27, '¿Cuál es la herramienta principal para martillar?', 'SINGLE', 1.00, 1),
(18, 28, '¿Qué micrófono es recomendado en este caso?', 'SINGLE', 1.00, 1),
(19, 29, '¿Qué micrófono no es recomendado en este caso?', 'SINGLE', 1.00, 1),
(23, 32, 'sdads', 'SINGLE', 1.00, 1),
(25, 33, 'dassdad', 'SINGLE', 1.00, 1),
(26, 34, 'aaa', 'SINGLE', 1.00, 1),
(27, 35, 'hola', 'SINGLE', 1.00, 1),
(28, 36, 'hola', 'SINGLE', 1.00, 1),
(29, 37, 'hola', 'SINGLE', 1.00, 1),
(30, 38, 'hola', 'SINGLE', 1.00, 1),
(31, 39, 'hola', 'SINGLE', 1.00, 1),
(32, 40, 'hola', 'SINGLE', 1.00, 1),
(33, 41, 'hola', 'SINGLE', 1.00, 1),
(34, 42, 'hola', 'SINGLE', 1.00, 1),
(35, 42, 'hola', 'SINGLE', 1.00, 2),
(36, 42, 'hola', 'SINGLE', 1.00, 3),
(37, 43, 'a', 'SINGLE', 1.00, 1),
(38, 44, 'a', 'SINGLE', 1.00, 1),
(39, 45, 'a', 'SINGLE', 1.00, 1),
(40, 46, 'dasdasda 1', 'SINGLE', 1.00, 1),
(41, 47, 'a', 'SINGLE', 1.00, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `roles`
--

INSERT INTO `roles` (`id`, `name`) VALUES
(1, 'Administrador'),
(2, 'Estudiante'),
(3, 'Profesor');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `schools`
--

CREATE TABLE `schools` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(120) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `schools`
--

INSERT INTO `schools` (`id`, `name`) VALUES
(1, 'Escuela de Administracion y Negocios'),
(2, 'Escuela de Comunicacion'),
(3, 'Escuela de Construccion'),
(4, 'Escuela de Diseño'),
(5, 'Escuela de Gastronomia'),
(6, 'Escuela de Informatica y Telecomunicaciones'),
(7, 'Escuela de Ingenieria y Recursos Naturales'),
(8, 'Escuela de Salud Y Bienestar'),
(9, 'Escuela de Turismo y Hospitalidad');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE `users` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `role_id` bigint(20) UNSIGNED NOT NULL,
  `category_id` bigint(20) UNSIGNED NOT NULL,
  `first_name` varchar(80) NOT NULL,
  `middle_name` varchar(80) DEFAULT NULL,
  `last_name` varchar(80) NOT NULL,
  `second_last_name` varchar(80) DEFAULT NULL,
  `email` varchar(160) NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT NULL,
  `deleted_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `users`
--

INSERT INTO `users` (`id`, `role_id`, `category_id`, `first_name`, `middle_name`, `last_name`, `second_last_name`, `email`, `is_active`, `created_at`, `updated_at`, `deleted_at`) VALUES
(15, 2, 58, 'benjamin', 'hernan', 'gamboa', 'araya', 'benjamin.gamboa.gb@gmail.com', 1, '2025-11-23 15:16:50', NULL, NULL),
(16, 3, 58, 'benjamin', 'hernan', 'gamboa', 'araya', 'be.gamboa@duocuc.cl', 1, '2025-11-23 16:57:26', NULL, NULL),
(23, 3, 58, 'Julio', NULL, 'Tapia', 'Acevedo', 'ben.profe@duocuc.cl', 1, '2025-11-27 22:00:27', '2025-11-27 19:02:06', NULL),
(24, 3, 58, 'profe', 'profe', 'profe', 'profe', 'profe@duocuc.cl', 1, '2025-11-27 19:02:58', '2025-11-27 19:04:29', NULL),
(25, 2, 58, 'benja estudiante', '', '', '', 'ben.guajardo.estudiante@duocuc.cl', 1, '2025-11-27 22:46:36', NULL, NULL),
(26, 2, 58, 'Nicolás Canales', 'Nicolás Canales', 'Nicolás Canales', 'Nicolás Canales', 'nicolas.canales@duocuc.cl', 1, '2025-11-27 23:10:17', '2025-11-27 20:19:46', NULL),
(27, 2, 58, 'benjamín', 'francisco', 'Guajardo', 'Parra', 'benjamin.guajardoparra@gmail.com', 1, '2025-11-27 23:17:04', NULL, NULL),
(100, 1, 1, 'admin', NULL, 'admin', NULL, 'admin@admin.cl', 1, '2025-12-09 07:44:41', NULL, NULL),
(101, 2, 58, 'Sebastián', 'Ignacio', 'Zapata', 'Zapata', 'thepyonbe@gmail.com', 1, '2025-12-09 15:50:53', NULL, NULL);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `answers`
--
ALTER TABLE `answers`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_ans_attempt` (`attempt_id`),
  ADD KEY `fk_ans_question` (`question_id`),
  ADD KEY `fk_ans_option` (`option_id`);

--
-- Indices de la tabla `attempts`
--
ALTER TABLE `attempts`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_att_eval` (`evaluation_id`),
  ADD KEY `fk_att_student` (`student_id`);

--
-- Indices de la tabla `auth_credentials`
--
ALTER TABLE `auth_credentials`
  ADD PRIMARY KEY (`user_id`);

--
-- Indices de la tabla `certificates`
--
ALTER TABLE `certificates`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `verification_code` (`verification_code`),
  ADD KEY `fk_cert_enr` (`enrollment_id`);

--
-- Indices de la tabla `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_course_teacher` (`teacher_id`),
  ADD KEY `fk_course_category` (`category_id`),
  ADD KEY `fk_course_school` (`school_id`);

--
-- Indices de la tabla `course_categories`
--
ALTER TABLE `course_categories`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `school_id` (`school_id`,`name`);

--
-- Indices de la tabla `course_modules`
--
ALTER TABLE `course_modules`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `course_id` (`course_id`,`position`);

--
-- Indices de la tabla `course_progress`
--
ALTER TABLE `course_progress`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `enrollment_id` (`enrollment_id`);

--
-- Indices de la tabla `enrollments`
--
ALTER TABLE `enrollments`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `student_id` (`student_id`,`course_id`),
  ADD KEY `fk_enr_course` (`course_id`);

--
-- Indices de la tabla `evaluations`
--
ALTER TABLE `evaluations`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_eval_course` (`course_id`),
  ADD KEY `fk_eval_lesson` (`lesson_id`);

--
-- Indices de la tabla `lessons`
--
ALTER TABLE `lessons`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `module_id` (`module_id`,`position`);

--
-- Indices de la tabla `lesson_progress`
--
ALTER TABLE `lesson_progress`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `enrollment_id` (`enrollment_id`,`lesson_id`),
  ADD KEY `fk_lprog_lesson` (`lesson_id`);

--
-- Indices de la tabla `media`
--
ALTER TABLE `media`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_media_course` (`course_id`),
  ADD KEY `fk_media_uploader` (`uploaded_by`);

--
-- Indices de la tabla `notifications`
--
ALTER TABLE `notifications`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_notif_user` (`user_id`);

--
-- Indices de la tabla `options`
--
ALTER TABLE `options`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_opt_q` (`question_id`);

--
-- Indices de la tabla `password_reset_tokens`
--
ALTER TABLE `password_reset_tokens`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_password_reset_user` (`user_id`);

--
-- Indices de la tabla `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `evaluation_id` (`evaluation_id`,`position`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indices de la tabla `schools`
--
ALTER TABLE `schools`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indices de la tabla `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `fk_users_role` (`role_id`),
  ADD KEY `fk_users_category` (`category_id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `answers`
--
ALTER TABLE `answers`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=71;

--
-- AUTO_INCREMENT de la tabla `attempts`
--
ALTER TABLE `attempts`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=67;

--
-- AUTO_INCREMENT de la tabla `certificates`
--
ALTER TABLE `certificates`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `courses`
--
ALTER TABLE `courses`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `course_categories`
--
ALTER TABLE `course_categories`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=78;

--
-- AUTO_INCREMENT de la tabla `course_modules`
--
ALTER TABLE `course_modules`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `course_progress`
--
ALTER TABLE `course_progress`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `enrollments`
--
ALTER TABLE `enrollments`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT de la tabla `evaluations`
--
ALTER TABLE `evaluations`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- AUTO_INCREMENT de la tabla `lessons`
--
ALTER TABLE `lessons`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `lesson_progress`
--
ALTER TABLE `lesson_progress`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `media`
--
ALTER TABLE `media`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `notifications`
--
ALTER TABLE `notifications`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `options`
--
ALTER TABLE `options`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=93;

--
-- AUTO_INCREMENT de la tabla `password_reset_tokens`
--
ALTER TABLE `password_reset_tokens`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT de la tabla `questions`
--
ALTER TABLE `questions`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

--
-- AUTO_INCREMENT de la tabla `roles`
--
ALTER TABLE `roles`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `schools`
--
ALTER TABLE `schools`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `users`
--
ALTER TABLE `users`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=102;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `answers`
--
ALTER TABLE `answers`
  ADD CONSTRAINT `fk_ans_attempt` FOREIGN KEY (`attempt_id`) REFERENCES `attempts` (`id`),
  ADD CONSTRAINT `fk_ans_option` FOREIGN KEY (`option_id`) REFERENCES `options` (`id`),
  ADD CONSTRAINT `fk_ans_question` FOREIGN KEY (`question_id`) REFERENCES `questions` (`id`);

--
-- Filtros para la tabla `attempts`
--
ALTER TABLE `attempts`
  ADD CONSTRAINT `fk_att_eval` FOREIGN KEY (`evaluation_id`) REFERENCES `evaluations` (`id`),
  ADD CONSTRAINT `fk_att_student` FOREIGN KEY (`student_id`) REFERENCES `users` (`id`);

--
-- Filtros para la tabla `auth_credentials`
--
ALTER TABLE `auth_credentials`
  ADD CONSTRAINT `fk_auth_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `certificates`
--
ALTER TABLE `certificates`
  ADD CONSTRAINT `fk_cert_enr` FOREIGN KEY (`enrollment_id`) REFERENCES `enrollments` (`id`);

--
-- Filtros para la tabla `courses`
--
ALTER TABLE `courses`
  ADD CONSTRAINT `fk_course_category` FOREIGN KEY (`category_id`) REFERENCES `course_categories` (`id`),
  ADD CONSTRAINT `fk_course_school` FOREIGN KEY (`school_id`) REFERENCES `schools` (`id`),
  ADD CONSTRAINT `fk_course_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `users` (`id`);

--
-- Filtros para la tabla `course_categories`
--
ALTER TABLE `course_categories`
  ADD CONSTRAINT `fk_cc_school` FOREIGN KEY (`school_id`) REFERENCES `schools` (`id`);

--
-- Filtros para la tabla `course_modules`
--
ALTER TABLE `course_modules`
  ADD CONSTRAINT `fk_cmodules_course` FOREIGN KEY (`course_id`) REFERENCES `courses` (`id`);

--
-- Filtros para la tabla `course_progress`
--
ALTER TABLE `course_progress`
  ADD CONSTRAINT `fk_cprogress_enr` FOREIGN KEY (`enrollment_id`) REFERENCES `enrollments` (`id`);

--
-- Filtros para la tabla `enrollments`
--
ALTER TABLE `enrollments`
  ADD CONSTRAINT `fk_enr_course` FOREIGN KEY (`course_id`) REFERENCES `courses` (`id`),
  ADD CONSTRAINT `fk_enr_student` FOREIGN KEY (`student_id`) REFERENCES `users` (`id`);

--
-- Filtros para la tabla `evaluations`
--
ALTER TABLE `evaluations`
  ADD CONSTRAINT `fk_eval_course` FOREIGN KEY (`course_id`) REFERENCES `courses` (`id`),
  ADD CONSTRAINT `fk_eval_lesson` FOREIGN KEY (`lesson_id`) REFERENCES `lessons` (`id`);

--
-- Filtros para la tabla `lessons`
--
ALTER TABLE `lessons`
  ADD CONSTRAINT `fk_lessons_module` FOREIGN KEY (`module_id`) REFERENCES `course_modules` (`id`);

--
-- Filtros para la tabla `lesson_progress`
--
ALTER TABLE `lesson_progress`
  ADD CONSTRAINT `fk_lprog_enr` FOREIGN KEY (`enrollment_id`) REFERENCES `enrollments` (`id`),
  ADD CONSTRAINT `fk_lprog_lesson` FOREIGN KEY (`lesson_id`) REFERENCES `lessons` (`id`);

--
-- Filtros para la tabla `media`
--
ALTER TABLE `media`
  ADD CONSTRAINT `fk_media_course` FOREIGN KEY (`course_id`) REFERENCES `courses` (`id`),
  ADD CONSTRAINT `fk_media_uploader` FOREIGN KEY (`uploaded_by`) REFERENCES `users` (`id`);

--
-- Filtros para la tabla `notifications`
--
ALTER TABLE `notifications`
  ADD CONSTRAINT `fk_notif_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

--
-- Filtros para la tabla `options`
--
ALTER TABLE `options`
  ADD CONSTRAINT `fk_opt_q` FOREIGN KEY (`question_id`) REFERENCES `questions` (`id`);

--
-- Filtros para la tabla `password_reset_tokens`
--
ALTER TABLE `password_reset_tokens`
  ADD CONSTRAINT `fk_password_reset_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `questions`
--
ALTER TABLE `questions`
  ADD CONSTRAINT `fk_q_eval` FOREIGN KEY (`evaluation_id`) REFERENCES `evaluations` (`id`);

--
-- Filtros para la tabla `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `fk_users_category` FOREIGN KEY (`category_id`) REFERENCES `course_categories` (`id`),
  ADD CONSTRAINT `fk_users_role` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
