
CREATE TABLE public.mobile_statistics (
	id uuid NOT NULL,
	title text NOT NULL,
	last_statistics date NOT NULL,
	version_client character varying(50),
	type character varying(50)
);
CREATE TABLE public.mobile_statistics_events (
	id uuid NOT NULL,
	mobile_statistics_id uuid NOT NULL,
	name character varying(50) NOT NULL,
	date date NOT NULL,
	description text
);