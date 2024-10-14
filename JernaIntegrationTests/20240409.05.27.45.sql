CREATE ROLE azure_pg_admin;
CREATE ROLE jernaschoolsadmin;
--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0
-- Dumped by pg_dump version 16.2 (Debian 16.2-1.pgdg120+2)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: public; Type: SCHEMA; Schema: -; Owner: azure_pg_admin
--

-- CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO azure_pg_admin;

--
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: azure_pg_admin
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: admin_history; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.admin_history (
    id integer NOT NULL,
    purchase_id integer,
    fulfilled_date timestamp without time zone NOT NULL,
    notes character varying(1000)
);


ALTER TABLE public.admin_history OWNER TO jernaschoolsadmin;

--
-- Name: admin_history_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.admin_history_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.admin_history_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: admin_history_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.admin_history_id_seq OWNED BY public.admin_history.id;


--
-- Name: admin_history_item; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.admin_history_item (
    id integer NOT NULL,
    admin_history_id integer,
    item_id integer
);


ALTER TABLE public.admin_history_item OWNER TO jernaschoolsadmin;

--
-- Name: admin_history_item_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.admin_history_item_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.admin_history_item_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: admin_history_item_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.admin_history_item_id_seq OWNED BY public.admin_history_item.id;


--
-- Name: auth_code; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.auth_code (
    id integer NOT NULL,
    code character varying(16) NOT NULL,
    is_valid_until date
);


ALTER TABLE public.auth_code OWNER TO jernaschoolsadmin;

--
-- Name: auth_code_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.auth_code_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.auth_code_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: auth_code_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.auth_code_id_seq OWNED BY public.auth_code.id;


--
-- Name: temp_code; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.temp_code (
    id integer NOT NULL,
    code character varying(16) NOT NULL,
    createdate timestamp without time zone NOT NULL,
    userid integer NOT NULL
);


ALTER TABLE public.temp_code OWNER TO jernaschoolsadmin;

--
-- Name: authcodes_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.authcodes_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.authcodes_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: authcodes_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.authcodes_id_seq OWNED BY public.temp_code.id;


--
-- Name: cart; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.cart (
    id integer NOT NULL,
    user_id integer
);


ALTER TABLE public.cart OWNER TO jernaschoolsadmin;

--
-- Name: cart_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.cart_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.cart_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: cart_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.cart_id_seq OWNED BY public.cart.id;


--
-- Name: cart_item; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.cart_item (
    id integer NOT NULL,
    cart_id integer,
    item_id integer,
    quantity integer NOT NULL,
    contact_info character varying(100)
);


ALTER TABLE public.cart_item OWNER TO jernaschoolsadmin;

--
-- Name: cart_item_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.cart_item_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.cart_item_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: cart_item_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.cart_item_id_seq OWNED BY public.cart_item.id;


--
-- Name: item; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.item (
    id integer NOT NULL,
    item_name character varying(80) NOT NULL,
    price money NOT NULL,
    description text,
    image bytea,
    isdisplayed boolean NOT NULL,
    mediafile bytea,
    is_digital boolean,
    is_physical boolean,
    period_length_id integer DEFAULT 1 NOT NULL,
    CONSTRAINT check_item_type CHECK ((is_digital OR is_physical))
);


ALTER TABLE public.item OWNER TO jernaschoolsadmin;

--
-- Name: item_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.item_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.item_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: item_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.item_id_seq OWNED BY public.item.id;


--
-- Name: period_length; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.period_length (
    id integer NOT NULL,
    display_name character varying(60),
    timespan interval NOT NULL
);


ALTER TABLE public.period_length OWNER TO jernaschoolsadmin;

--
-- Name: period_length_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.period_length_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.period_length_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: period_length_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.period_length_id_seq OWNED BY public.period_length.id;


--
-- Name: purchase; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.purchase (
    id integer NOT NULL,
    user_id integer,
    purchase_date timestamp without time zone NOT NULL,
    taxpercent double precision NOT NULL
);


ALTER TABLE public.purchase OWNER TO jernaschoolsadmin;

--
-- Name: purchase_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.purchase_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.purchase_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: purchase_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.purchase_id_seq OWNED BY public.purchase.id;


--
-- Name: purchase_item; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.purchase_item (
    id integer NOT NULL,
    purchase_id integer,
    item_id integer,
    quantity integer NOT NULL
);


ALTER TABLE public.purchase_item OWNER TO jernaschoolsadmin;

--
-- Name: purchase_item_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.purchase_item_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.purchase_item_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: purchase_item_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.purchase_item_id_seq OWNED BY public.purchase_item.id;


--
-- Name: tag; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.tag (
    id integer NOT NULL,
    tag_name character varying(80),
    description text
);


ALTER TABLE public.tag OWNER TO jernaschoolsadmin;

--
-- Name: tag_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.tag_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tag_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: tag_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.tag_id_seq OWNED BY public.tag.id;


--
-- Name: tag_item; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.tag_item (
    id integer NOT NULL,
    tag_id integer,
    item_id integer
);


ALTER TABLE public.tag_item OWNER TO jernaschoolsadmin;

--
-- Name: tag_item_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.tag_item_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tag_item_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: tag_item_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.tag_item_id_seq OWNED BY public.tag_item.id;


--
-- Name: tools_for_parents_item; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.tools_for_parents_item (
    id integer NOT NULL,
    sub_id integer NOT NULL,
    item_id integer NOT NULL
);


ALTER TABLE public.tools_for_parents_item OWNER TO jernaschoolsadmin;

--
-- Name: tools_for_parents_item_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.tools_for_parents_item_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tools_for_parents_item_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: tools_for_parents_item_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.tools_for_parents_item_id_seq OWNED BY public.tools_for_parents_item.id;


--
-- Name: transaction; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.transaction (
    id integer NOT NULL,
    purchase_price money,
    purchase_id integer
);


ALTER TABLE public.transaction OWNER TO jernaschoolsadmin;

--
-- Name: transaction_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.transaction_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.transaction_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: transaction_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.transaction_id_seq OWNED BY public.transaction.id;


--
-- Name: user; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public."user" (
    id integer NOT NULL,
    username character varying(64) NOT NULL,
    email character varying(80) NOT NULL,
    notes text,
    isadmin boolean DEFAULT false
);


ALTER TABLE public."user" OWNER TO jernaschoolsadmin;

--
-- Name: user_auth_code; Type: TABLE; Schema: public; Owner: jernaschoolsadmin
--

CREATE TABLE public.user_auth_code (
    id integer NOT NULL,
    user_id integer,
    auth_code_id integer
);


ALTER TABLE public.user_auth_code OWNER TO jernaschoolsadmin;

--
-- Name: user_auth_code_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.user_auth_code_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.user_auth_code_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: user_auth_code_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.user_auth_code_id_seq OWNED BY public.user_auth_code.id;


--
-- Name: user_id_seq; Type: SEQUENCE; Schema: public; Owner: jernaschoolsadmin
--

CREATE SEQUENCE public.user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.user_id_seq OWNER TO jernaschoolsadmin;

--
-- Name: user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jernaschoolsadmin
--

ALTER SEQUENCE public.user_id_seq OWNED BY public."user".id;


--
-- Name: admin_history id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.admin_history ALTER COLUMN id SET DEFAULT nextval('public.admin_history_id_seq'::regclass);


--
-- Name: admin_history_item id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.admin_history_item ALTER COLUMN id SET DEFAULT nextval('public.admin_history_item_id_seq'::regclass);


--
-- Name: auth_code id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.auth_code ALTER COLUMN id SET DEFAULT nextval('public.auth_code_id_seq'::regclass);


--
-- Name: cart id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.cart ALTER COLUMN id SET DEFAULT nextval('public.cart_id_seq'::regclass);


--
-- Name: cart_item id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.cart_item ALTER COLUMN id SET DEFAULT nextval('public.cart_item_id_seq'::regclass);


--
-- Name: item id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.item ALTER COLUMN id SET DEFAULT nextval('public.item_id_seq'::regclass);


--
-- Name: period_length id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.period_length ALTER COLUMN id SET DEFAULT nextval('public.period_length_id_seq'::regclass);


--
-- Name: purchase id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.purchase ALTER COLUMN id SET DEFAULT nextval('public.purchase_id_seq'::regclass);


--
-- Name: purchase_item id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.purchase_item ALTER COLUMN id SET DEFAULT nextval('public.purchase_item_id_seq'::regclass);


--
-- Name: tag id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tag ALTER COLUMN id SET DEFAULT nextval('public.tag_id_seq'::regclass);


--
-- Name: tag_item id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tag_item ALTER COLUMN id SET DEFAULT nextval('public.tag_item_id_seq'::regclass);


--
-- Name: temp_code id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.temp_code ALTER COLUMN id SET DEFAULT nextval('public.authcodes_id_seq'::regclass);


--
-- Name: tools_for_parents_item id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tools_for_parents_item ALTER COLUMN id SET DEFAULT nextval('public.tools_for_parents_item_id_seq'::regclass);


--
-- Name: transaction id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.transaction ALTER COLUMN id SET DEFAULT nextval('public.transaction_id_seq'::regclass);


--
-- Name: user id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public."user" ALTER COLUMN id SET DEFAULT nextval('public.user_id_seq'::regclass);


--
-- Name: user_auth_code id; Type: DEFAULT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.user_auth_code ALTER COLUMN id SET DEFAULT nextval('public.user_auth_code_id_seq'::regclass);


--
-- Data for Name: admin_history; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.admin_history (id, purchase_id, fulfilled_date, notes) FROM stdin;
\.


--
-- Data for Name: admin_history_item; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.admin_history_item (id, admin_history_id, item_id) FROM stdin;
\.


--
-- Data for Name: auth_code; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.auth_code (id, code, is_valid_until) FROM stdin;
\.


--
-- Data for Name: cart; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.cart (id, user_id) FROM stdin;
\.


--
-- Data for Name: cart_item; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.cart_item (id, cart_id, item_id, quantity, contact_info) FROM stdin;
\.


--
-- Data for Name: item; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.item (id, item_name, price, description, image, isdisplayed, mediafile, is_digital, is_physical, period_length_id) FROM stdin;
63	Learning the Number Words with the Dinosaurs  	$1.50	Learn and use the number words zero – ten	\N	t	\N	t	\N	1
64	Learning the Letters with the Dinosaurs	$1.50	Learn and use the letters of the alphabet to begin reading. 	\N	t	\N	t	\N	1
28	Dino Box	$11.00	\N	\N	f	\N	\N	t	4
30	Dino Box	$42.00	\N	\N	f	\N	t	t	2
31	Dino Box	$21.75	\N	\N	f	\N	t	t	3
32	Dino Box	$11.50	\N	\N	f	\N	t	t	4
59	Plantegory	$12.00	This helps you learn about animals, environments, and logic.  	\N	t	\N	\N	t	1
54	Cookie Monster Matching Games	$29.00	This helps you practice colors, develop matching skills, and practice recognizing patterns.  	\N	t	\N	\N	t	1
60	Stacking Dinosaurs	$1.00	This helps you learn about dinosaurs and practice counting.  	\N	t	\N	t	\N	1
53	Building Bricks Boxes - Bundle Numbers and Words-Maze Games	$28.00	The bundle helps you practice spelling numerals, single digit addition and subtraction, practice spelling words, build logic skills, and increase creativity. 	\N	t	\N	\N	t	1
61	Stacking Dinosaurs	$12.00	This helps you learn about dinosaurs and practice counting.  	\N	t	\N	\N	t	1
58	Plantegory	$1.00	This helps you learn about animals, environments, and logic.  	\N	t	\N	t	\N	1
102	a	$0.00	\N	\N	f	\N	\N	\N	1
103	a	$0.00	\N	\N	f	\N	\N	\N	1
106	aa	$0.00	\N	\N	f	\N	\N	\N	1
110	a	$0.00	\N	\N	f	\N	\N	\N	1
113	a	$0.00	\N	\N	f	\N	\N	\N	1
117	a	$0.00	\N	\N	f	\N	\N	\N	1
119	a	$0.00	\N	\N	f	\N	\N	\N	1
120	a	$0.00	\N	\N	f	\N	\N	\N	1
121	a	$0.00	\N	\N	f	\N	\N	\N	1
122	a	$0.00	\N	\N	f	\N	\N	\N	1
123	b	$0.00	\N	\N	f	\N	\N	\N	1
124	a	$0.00	\N	\N	f	\N	\N	\N	1
51	Building Brick Box - Numbers and Words	$20.00	This helps you practice spelling numerals, single digit addition and subtraction, and practice spelling words	\N	t	\N	\N	t	1
\.


--
-- Data for Name: period_length; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.period_length (id, display_name, timespan) FROM stdin;
2	weekly	7 days
3	biweekly	14 days
4	monthly	1 mon
1	once	00:00:00
\.


--
-- Data for Name: purchase; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.purchase (id, user_id, purchase_date, taxpercent) FROM stdin;
\.


--
-- Data for Name: purchase_item; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.purchase_item (id, purchase_id, item_id, quantity) FROM stdin;
\.


--
-- Data for Name: tag; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.tag (id, tag_name, description) FROM stdin;
9	Animals	Anything related to animals
10	Physical	Any product that can be shipped to your home or held in your hand
11	Digital	Any product that can only be transferred through digital formats. These products can be printed.
14	Homeschool resources	Various resources to support homeschooling parents and students.
24	Elementary	Content and resources suitable for elementary school students.
25	Preschool	Resources and activities tailored for homeschooling preschoolers.
26	Kindergarten	Educational materials and activities for homeschooling kindergarteners.
13	Science	Engaging science-related content suitable for children.
15	Subscription	Subscription service providing educational materials and updates.
17	Nature	Exploration and learning activities centered around the natural world.
20	Activities	Hands-on activities and projects suitable for homeschooling.
22	Lessons	Structured lessons and curriculum for homeschooling.
23	Printable	Printable worksheets and activities for children.
28	Tools	Tools and resources to facilitate homeschooling.
29	Math	Interactive games and activities to teach mathematics.
30	Alphabet	Games and activities focused on learning the alphabet.
31	Games	Games designed to facilitate learning and education.
32	Memory	Games aimed at improving memory and cognitive skills.
33	Toys	Toys designed to promote learning and education.
42	Curriculum	Structured curriculum designed for homeschooling.
27	Grade School	Resources for grade school.
46	Language Arts	\N
47	Logic	\N
48	Creativity	\N
7	Dinosaurs	Anything related to dinosaurs
\.


--
-- Data for Name: tag_item; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.tag_item (id, tag_id, item_id) FROM stdin;
\.


--
-- Data for Name: temp_code; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.temp_code (id, code, createdate, userid) FROM stdin;
\.


--
-- Data for Name: tools_for_parents_item; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.tools_for_parents_item (id, sub_id, item_id) FROM stdin;
\.


--
-- Data for Name: transaction; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.transaction (id, purchase_price, purchase_id) FROM stdin;
\.


--
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public."user" (id, username, email, notes, isadmin) FROM stdin;
\.


--
-- Data for Name: user_auth_code; Type: TABLE DATA; Schema: public; Owner: jernaschoolsadmin
--

COPY public.user_auth_code (id, user_id, auth_code_id) FROM stdin;
\.


--
-- Name: admin_history_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.admin_history_id_seq', 1, false);


--
-- Name: admin_history_item_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.admin_history_item_id_seq', 1, false);


--
-- Name: auth_code_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.auth_code_id_seq', 118, true);


--
-- Name: authcodes_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.authcodes_id_seq', 112, true);


--
-- Name: cart_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.cart_id_seq', 41, true);


--
-- Name: cart_item_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.cart_item_id_seq', 91, true);


--
-- Name: item_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.item_id_seq', 124, true);


--
-- Name: period_length_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.period_length_id_seq', 4, true);


--
-- Name: purchase_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.purchase_id_seq', 2, true);


--
-- Name: purchase_item_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.purchase_item_id_seq', 2, true);


--
-- Name: tag_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.tag_id_seq', 48, true);


--
-- Name: tag_item_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.tag_item_id_seq', 29, true);


--
-- Name: tools_for_parents_item_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.tools_for_parents_item_id_seq', 1, false);


--
-- Name: transaction_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.transaction_id_seq', 1, true);


--
-- Name: user_auth_code_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.user_auth_code_id_seq', 118, true);


--
-- Name: user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: jernaschoolsadmin
--

SELECT pg_catalog.setval('public.user_id_seq', 154, true);


--
-- Name: admin_history_item admin_history_item_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.admin_history_item
    ADD CONSTRAINT admin_history_item_pkey PRIMARY KEY (id);


--
-- Name: admin_history admin_history_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.admin_history
    ADD CONSTRAINT admin_history_pkey PRIMARY KEY (id);


--
-- Name: auth_code auth_code_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.auth_code
    ADD CONSTRAINT auth_code_pkey PRIMARY KEY (id);


--
-- Name: temp_code authcodes_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.temp_code
    ADD CONSTRAINT authcodes_pkey PRIMARY KEY (id);


--
-- Name: cart_item cart_item_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.cart_item
    ADD CONSTRAINT cart_item_pkey PRIMARY KEY (id);


--
-- Name: cart cart_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.cart
    ADD CONSTRAINT cart_pkey PRIMARY KEY (id);


--
-- Name: item item_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.item
    ADD CONSTRAINT item_pkey PRIMARY KEY (id);


--
-- Name: period_length period_length_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.period_length
    ADD CONSTRAINT period_length_pkey PRIMARY KEY (id);


--
-- Name: purchase_item purchase_item_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.purchase_item
    ADD CONSTRAINT purchase_item_pkey PRIMARY KEY (id);


--
-- Name: purchase purchase_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.purchase
    ADD CONSTRAINT purchase_pkey PRIMARY KEY (id);


--
-- Name: tag_item tag_item_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tag_item
    ADD CONSTRAINT tag_item_pkey PRIMARY KEY (id);


--
-- Name: tag tag_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tag
    ADD CONSTRAINT tag_pkey PRIMARY KEY (id);


--
-- Name: tag tag_un; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tag
    ADD CONSTRAINT tag_un UNIQUE (tag_name);


--
-- Name: tools_for_parents_item tools_for_parents_item_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tools_for_parents_item
    ADD CONSTRAINT tools_for_parents_item_pkey PRIMARY KEY (id);


--
-- Name: transaction transaction_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.transaction
    ADD CONSTRAINT transaction_pkey PRIMARY KEY (id);


--
-- Name: user_auth_code user_auth_code_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.user_auth_code
    ADD CONSTRAINT user_auth_code_pkey PRIMARY KEY (id);


--
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);


--
-- Name: user user_un; Type: CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_un UNIQUE (email);


--
-- Name: admin_history_item admin_history_item_admin_history_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.admin_history_item
    ADD CONSTRAINT admin_history_item_admin_history_id_fkey FOREIGN KEY (admin_history_id) REFERENCES public.admin_history(id);


--
-- Name: admin_history_item admin_history_item_item_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.admin_history_item
    ADD CONSTRAINT admin_history_item_item_id_fkey FOREIGN KEY (item_id) REFERENCES public.item(id);


--
-- Name: admin_history admin_history_purchase_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.admin_history
    ADD CONSTRAINT admin_history_purchase_id_fkey FOREIGN KEY (purchase_id) REFERENCES public.purchase(id);


--
-- Name: temp_code authcodes_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.temp_code
    ADD CONSTRAINT authcodes_userid_fkey FOREIGN KEY (userid) REFERENCES public."user"(id);


--
-- Name: cart_item cart_item_cart_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.cart_item
    ADD CONSTRAINT cart_item_cart_id_fkey FOREIGN KEY (cart_id) REFERENCES public.cart(id);


--
-- Name: cart_item cart_item_item_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.cart_item
    ADD CONSTRAINT cart_item_item_id_fkey FOREIGN KEY (item_id) REFERENCES public.item(id);


--
-- Name: cart cart_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.cart
    ADD CONSTRAINT cart_user_id_fkey FOREIGN KEY (user_id) REFERENCES public."user"(id);


--
-- Name: item fk_period_length_id; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.item
    ADD CONSTRAINT fk_period_length_id FOREIGN KEY (period_length_id) REFERENCES public.period_length(id);


--
-- Name: purchase_item purchase_item_item_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.purchase_item
    ADD CONSTRAINT purchase_item_item_id_fkey FOREIGN KEY (item_id) REFERENCES public.item(id);


--
-- Name: purchase_item purchase_item_purchase_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.purchase_item
    ADD CONSTRAINT purchase_item_purchase_id_fkey FOREIGN KEY (purchase_id) REFERENCES public.purchase(id);


--
-- Name: purchase purchase_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.purchase
    ADD CONSTRAINT purchase_user_id_fkey FOREIGN KEY (user_id) REFERENCES public."user"(id);


--
-- Name: tag_item tag_item_item_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tag_item
    ADD CONSTRAINT tag_item_item_id_fkey FOREIGN KEY (item_id) REFERENCES public.item(id);


--
-- Name: tag_item tag_item_tag_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tag_item
    ADD CONSTRAINT tag_item_tag_id_fkey FOREIGN KEY (tag_id) REFERENCES public.tag(id);


--
-- Name: tools_for_parents_item tools_for_parents_item_item_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tools_for_parents_item
    ADD CONSTRAINT tools_for_parents_item_item_id_fkey FOREIGN KEY (item_id) REFERENCES public.item(id);


--
-- Name: tools_for_parents_item tools_for_parents_item_sub_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.tools_for_parents_item
    ADD CONSTRAINT tools_for_parents_item_sub_id_fkey FOREIGN KEY (sub_id) REFERENCES public.item(id);


--
-- Name: transaction transaction_purchase_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.transaction
    ADD CONSTRAINT transaction_purchase_id_fkey FOREIGN KEY (purchase_id) REFERENCES public.purchase(id);


--
-- Name: user_auth_code user_auth_code_auth_code_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.user_auth_code
    ADD CONSTRAINT user_auth_code_auth_code_id_fkey FOREIGN KEY (auth_code_id) REFERENCES public.auth_code(id);


--
-- Name: user_auth_code user_auth_code_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jernaschoolsadmin
--

ALTER TABLE ONLY public.user_auth_code
    ADD CONSTRAINT user_auth_code_user_id_fkey FOREIGN KEY (user_id) REFERENCES public."user"(id);


--
-- PostgreSQL database dump complete
--

