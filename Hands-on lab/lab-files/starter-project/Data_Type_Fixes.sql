ALTER TABLE EMPLOYEES MODIFY (Employeeid number);
ALTER TABLE EMPLOYEES MODIFY (Reportsto number);
ALTER TABLE ORDERS MODIFY (Employeeid number);

SELECT LISTAGG(table_name, ',')
FROM user_tables
ORDER BY table_name;