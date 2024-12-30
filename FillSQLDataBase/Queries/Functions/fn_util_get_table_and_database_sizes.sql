CREATE OR REPLACE FUNCTION fn_util_get_table_and_database_sizes()
    RETURNS TABLE(table_name text, row_count bigint, table_size text, row_size text, database_size text)
    LANGUAGE plpgsql
AS
$$
DECLARE
r RECORD;
    single_row_size BIGINT;
    db_size TEXT;
BEGIN
    -- Get the total size of the database
SELECT pg_size_pretty(pg_database_size(current_database())) INTO db_size;

-- Loop through each table in the public schema
FOR r IN
SELECT t.table_name
FROM information_schema.tables t
WHERE t.table_schema = 'public' AND t.table_type = 'BASE TABLE'
    LOOP
            -- Get the size of a single row for each table by selecting one row and calculating its size
            EXECUTE format('SELECT pg_column_size(t.*) FROM public.%I t LIMIT 1', r.table_name) INTO single_row_size;

-- Return the results for the current table
RETURN NEXT (
                         r.table_name,
                         (SELECT COUNT(*) FROM public."||r.table_name||"),  -- Row count
                         pg_size_pretty(pg_total_relation_size('public.' || r.table_name)),  -- Table size
                         pg_size_pretty(single_row_size),  -- Row size
                         db_size  -- Database size
                );
END LOOP;

    -- End of function
    RETURN;
END;
$$;

-- Alter function owner to postgres
ALTER FUNCTION fn_util_get_table_and_database_sizes() OWNER TO postgres;
