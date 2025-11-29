-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 29-11-2025 a las 16:30:49
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
(14, 8, 16, 38, NULL, NULL, NULL),
(15, 8, 17, 40, NULL, NULL, NULL),
(16, 9, 18, 45, NULL, NULL, NULL),
(17, 9, 19, 48, NULL, NULL, NULL);

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
(8, 26, 25, '2025-11-27 22:53:39', '2025-11-27 22:53:39', 100.00),
(9, 28, 26, '2025-11-27 23:14:39', '2025-11-27 23:14:39', 50.00);

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
(21, '$2a$11$Rjvl10IAYISVgaPY8f9i..yP6Sz8KlV4NsutsI.iW98oD5Nxu1y/.', 0, '2025-11-27 20:18:24', '2025-11-27 19:13:11', '2025-11-27 19:13:11', '2025-11-27 20:18:24'),
(23, '$2a$11$q0kBpowL8AaEQLTczBleb.mIMp/3pkJYirewPgLVtwAvdgsofqtjG', 0, NULL, '2025-11-27 22:00:28', '2025-11-27 22:00:28', '2025-11-27 19:01:28'),
(24, '$2a$11$jkJo4lwDadmQQJZM1OzcmuSDqwrvVOLRlVyKCRf829vn5xl7vK9pO', 0, NULL, '0000-00-00 00:00:00', '2025-11-27 19:02:58', '2025-11-27 19:04:29'),
(25, '$2a$11$wHl8XIadYzszFc3eNT7oCeTw9107zFy6B4pQ7j/U3qaS.7ByIBMPS', 0, NULL, '2025-11-27 22:46:36', '2025-11-27 22:46:36', '2025-11-27 22:46:36'),
(26, '$2a$11$qJU9A1q1mWBooAQ8ObZIt.mkYn/ktAVp6Ic7m1z3uAe40lLcEjT1.', 0, NULL, '2025-11-27 23:10:18', '2025-11-27 23:10:18', '2025-11-27 23:10:18'),
(27, '$2a$11$gydnM7reCA3Pc0fVdXbuxO4epx7OBX4HwPiihMYhRhcdUh9bfH.Vq', 0, NULL, '2025-11-27 20:17:45', '2025-11-27 23:17:04', '2025-11-27 20:17:45'),
(28, '$2a$11$8RkIjGTY86.NzzQqKO6I6er8p55NmzRCVdrN2hGbnK9jMs54k9rUa', 0, '2025-11-27 20:19:28', '0000-00-00 00:00:00', '2025-11-27 20:19:03', '2025-11-27 20:19:28');

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
(12, 'Microfonía', 'Microfonía', NULL, 23, 3, 2, NULL, NULL, 0, '2025-11-27 23:11:21', NULL);

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
(8, 12, 'Módulo 1', 1);

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
(11, 26, 12, '2025-11-27 23:13:51', 'IN_PROGRESS');

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
(30, 12, NULL, 'Evaluación final microfonía', 'Evaluación final microfonía', NULL, '', 0.6000, 1, '2025-11-27 23:15:43', NULL, 0);

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
(9, 8, 'Clase 1', 'https://localhost:7037/media/courses/12/lessons/9/31a1f39b-cd82-4906-83a3-c61b89844290.mp4', NULL, 1);

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
(49, 20, 'dasdasd', 0),
(50, 20, 'asdadsad', 1),
(51, 21, 'no', 0),
(52, 21, 'si', 1);

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
(17, 27, 'i0l5LlfvPFeZp9H6brlYYiba9dKruwrVWsoS02KPN7NM3w7fLekqYILyl5z9tPh1CghUE5JfJxJGig3YE_gB_Q', '2025-11-28 00:21:16', 0);

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
(20, 30, 'asdasdsad', 'SINGLE', 1.00, 1),
(21, 30, 'hola', 'SINGLE', 1.00, 2);

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

INSERT INTO `users` (`id`, `role_id`, `first_name`, `middle_name`, `last_name`, `second_last_name`, `email`, `is_active`, `created_at`, `updated_at`, `deleted_at`) VALUES
(15, 2, 'benjamin', 'hernan', 'gamboa', 'araya', 'benjamin.gamboa.gb@gmail.com', 1, '2025-11-23 15:16:50', NULL, NULL),
(16, 3, 'benjamin', 'hernan', 'gamboa', 'araya', 'be.gamboa@duocuc.cl', 1, '2025-11-23 16:57:26', NULL, NULL),
(21, 1, 'admin', '', '', '', 'admin@admin.cl', 1, '2025-11-27 19:13:11', NULL, NULL),
(23, 3, 'BenjaProfe', 'BenjaProfe', 'BenjaProfe', 'BenjaProfe', 'ben.profe@duocuc.cl', 1, '2025-11-27 22:00:27', '2025-11-27 19:02:06', NULL),
(24, 3, 'profe', 'profe', 'profe', 'profe', 'profe@duocuc.cl', 1, '2025-11-27 19:02:58', '2025-11-27 19:04:29', NULL),
(25, 2, 'benja estudiante', '', '', '', 'ben.guajardo.estudiante@duocuc.cl', 1, '2025-11-27 22:46:36', NULL, NULL),
(26, 2, 'Nicolás Canales', 'Nicolás Canales', 'Nicolás Canales', 'Nicolás Canales', 'nicolas.canales@duocuc.cl', 1, '2025-11-27 23:10:17', '2025-11-27 20:19:46', NULL),
(27, 2, 'benja', '', '', '', 'benjamin.guajardoparra@gmail.com', 1, '2025-11-27 23:17:04', NULL, NULL),
(28, 1, 'test', 'test', 'test', 'test', 'test@duocuc.cl', 1, '2025-11-27 20:19:03', NULL, NULL);

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
  ADD KEY `fk_users_role` (`role_id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `answers`
--
ALTER TABLE `answers`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `attempts`
--
ALTER TABLE `attempts`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `certificates`
--
ALTER TABLE `certificates`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `courses`
--
ALTER TABLE `courses`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `course_categories`
--
ALTER TABLE `course_categories`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=78;

--
-- AUTO_INCREMENT de la tabla `course_modules`
--
ALTER TABLE `course_modules`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `course_progress`
--
ALTER TABLE `course_progress`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `enrollments`
--
ALTER TABLE `enrollments`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `evaluations`
--
ALTER TABLE `evaluations`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT de la tabla `lessons`
--
ALTER TABLE `lessons`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

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
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=53;

--
-- AUTO_INCREMENT de la tabla `password_reset_tokens`
--
ALTER TABLE `password_reset_tokens`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `questions`
--
ALTER TABLE `questions`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

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
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

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
  ADD CONSTRAINT `fk_users_role` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
