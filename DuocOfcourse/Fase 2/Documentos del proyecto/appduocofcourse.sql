-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 11-11-2025 a las 01:34:07
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.0.30

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
(9, '$2a$11$1GEP8.wOtvejMRhTxRWPc.L8kSar1XBcUtHyDRbroVEwPUjaaj6Yu', 0, NULL, '2025-11-09 18:54:52', '2025-11-09 18:54:52', NULL),
(10, '$2a$11$.PSKzJSrFe079sJAepVVoOJcK3i8Q.iVF6fCWQHRjB.WjbAngcFT6', 0, NULL, '2025-11-09 19:03:41', '2025-11-09 19:03:41', '2025-11-09 19:03:41');

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
  `teacher_id` bigint(20) UNSIGNED NOT NULL,
  `category_id` bigint(20) UNSIGNED NOT NULL,
  `starts_at` datetime DEFAULT NULL,
  `ends_at` datetime DEFAULT NULL,
  `is_published` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  `updated_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
(1, 4, 'G6FaAc6x0DLiFCOPQLBHUbEwoDzjQyfJwAqSlWZo1QB1aBO2aFXPiEr8an9KnIDOZuk7HLq/wg7rlu2JcEEHzg==', '2025-11-09 02:38:13', 0);

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
(4, 1, 'Benjamín', 'Hernán', 'Gamboa', 'Araya', 'be.gamboa@duocuc.cl', 1, '2025-11-08 22:31:51', '2025-11-08 22:31:51', NULL),
(7, 1, 'Benjamín', 'Hernán', 'Gamboa', 'Araya', 'benjamin.gamboa.gb@gmail.com', 1, '2025-11-09 11:34:22', '2025-11-09 11:34:22', NULL),
(8, 2, 'benjamin', 'hernan', 'gamboa', 'araya', 'be.gamboaa@duocuc.cl', 1, '2025-11-09 18:46:48', NULL, NULL),
(9, 2, 'benjamin', 'hernan', 'gamboa', 'araya', 'benjamin@duocuc.cl', 1, '2025-11-09 18:54:51', NULL, NULL),
(10, 2, 'benjamin', 'hernan', 'gamboa', 'araya', 'gamboa@duocuc.cl', 1, '2025-11-09 19:03:41', NULL, NULL);

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
  ADD KEY `fk_course_category` (`category_id`);

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
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `attempts`
--
ALTER TABLE `attempts`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `certificates`
--
ALTER TABLE `certificates`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `courses`
--
ALTER TABLE `courses`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `course_categories`
--
ALTER TABLE `course_categories`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=78;

--
-- AUTO_INCREMENT de la tabla `course_modules`
--
ALTER TABLE `course_modules`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `course_progress`
--
ALTER TABLE `course_progress`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `enrollments`
--
ALTER TABLE `enrollments`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `evaluations`
--
ALTER TABLE `evaluations`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `lessons`
--
ALTER TABLE `lessons`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `lesson_progress`
--
ALTER TABLE `lesson_progress`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `media`
--
ALTER TABLE `media`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `notifications`
--
ALTER TABLE `notifications`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `options`
--
ALTER TABLE `options`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `password_reset_tokens`
--
ALTER TABLE `password_reset_tokens`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `questions`
--
ALTER TABLE `questions`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

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
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

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
