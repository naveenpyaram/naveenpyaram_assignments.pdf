import java.util.ArrayList;
import java.util.Scanner;

public class Tasks {

    public String title;
    public String description;

    public Tasks() {}

    public Tasks(String title, String description) {
        this.title = title;
        this.description = description;
    }

    @Override
    public String toString() {
        return "Title: " + title + ", Description: " + description;
    }

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        ArrayList<Tasks> tasks = new ArrayList<>();
        boolean exit = true;

        while (exit) {
            System.out.println("1. Add Task");
            System.out.println("2. Update Task");
            System.out.println("3. Delete Task");
            System.out.println("4. Read Tasks");
            System.out.println("5. Exit");

            System.out.print("Choose an option: ");
            int choice = Integer.parseInt(scanner.nextLine());

            switch (choice) {
                case 1:
                    create(scanner, tasks);
                    break;
                case 2:
                    update(scanner, tasks);
                    break;
                case 3:
                    delete(scanner, tasks);
                    break;
                case 4:
                    read(tasks);
                    break;
                case 5:
                    exit = false;
                    break;
                default:
                    System.out.println("Invalid input, please try again");
            }
        }

        System.out.println("Exiting the application");
        scanner.close();
    }

    public static void create(Scanner scanner, ArrayList<Tasks> tasks) {
        System.out.print("Enter title: ");
        String title = scanner.nextLine();
        System.out.print("Enter description: ");
        String description = scanner.nextLine();

        Tasks task = new Tasks(title, description);
        tasks.add(task);
        System.out.println("Task and description added successfully");
    }

    public static void update(Scanner scanner, ArrayList<Tasks> tasks) {
        System.out.print("Enter the index of task to be updated: ");
        int index = Integer.parseInt(scanner.nextLine());

        if (index < 0 || index >= tasks.size()) {
            System.out.println("Invalid index");
            return;
        }

        System.out.print("Enter new title: ");
        String newTitle = scanner.nextLine();
        System.out.print("Enter new description: ");
        String newDescription = scanner.nextLine();

        tasks.get(index).title = newTitle;
        tasks.get(index).description = newDescription;
        System.out.println("Task and description updated successfully");
    }

    public static void delete(Scanner scanner, ArrayList<Tasks> tasks) {
        System.out.print("Enter the index of task to be deleted: ");
        int index = Integer.parseInt(scanner.nextLine());

        if (index < 0 || index >= tasks.size()) {
            System.out.println("Invalid index");
            return;
        }

        tasks.remove(index);
        System.out.println("Task and description deleted successfully");
    }

    public static void read(ArrayList<Tasks> tasks) {
        if (tasks.isEmpty()) {
            System.out.println("This list is empty");
        } else {
            System.out.println("Tasks and descriptions in the list:");
            for (int i = 0; i < tasks.size(); i++) {
                System.out.println(i + ". " + tasks.get(i));
            }
        }
    }
}
