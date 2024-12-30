create function fn_util_find_all_enabled_triggers(p_schema_name character varying DEFAULT NULL::character varying, p_table_name character varying DEFAULT NULL::character varying)
    returns TABLE(schema_name character varying, table_name character varying, trigger_name character varying, trigger_definition text)
    language plpgsql
as
$$
BEGIN
RETURN QUERY
SELECT
    n.nspname::VARCHAR AS schema_name,  -- Ensure it's explicitly cast to VARCHAR
        c.relname::VARCHAR AS table_name,    -- Ensure it's explicitly cast to VARCHAR
        tgname::VARCHAR AS trigger_name,     -- Ensure it's explicitly cast to VARCHAR
        pg_catalog.pg_get_triggerdef(t.oid)::TEXT AS trigger_definition  -- Ensure it's explicitly cast to TEXT
FROM
    pg_catalog.pg_trigger t
        LEFT JOIN pg_catalog.pg_class c ON c.oid = t.tgrelid
        LEFT JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE
    t.tgenabled = 'O'  -- Only enabled triggers
  AND (p_schema_name IS NULL OR n.nspname = p_schema_name)  -- Optional schema filter
  AND (p_table_name IS NULL OR c.relname = p_table_name)  -- Optional table filter
ORDER BY
    schema_name, table_name, trigger_name;
END;
$$;

alter function fn_util_find_all_enabled_triggers(varchar, varchar) owner to postgres;

