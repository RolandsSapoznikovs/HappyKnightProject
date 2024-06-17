import java.util.Scanner;

public class test {
    public static void main(String[] args) {
        System.out.println("Andrejs Leja RDBF0 231RDB151");

        int num;

        System.out.print("Ievadiet uzdevuma numuru (1 vai 2): ");
        Scanner sc = new Scanner(System.in);
        num = sc.nextInt();

        switch (num) {
            case 1 :
                int[][] a = new int[10][10];
                int n = 1;
                for (int i = 9; i >= 0 ;i--) { //int i = 0; i <= 9 ;i++
                    for (int j = 9 - i; j <= i - 7 ; j++) { //int j = 9-i; j >= 7 - i; j--
                        if (j >= 0) {
                            a[j][i] = n++;
                        }
                    }
                }

                // Display the array in a table-like format
                for (int j = 0; j < 10; j++) {
                    for (int i = 0; i < 10; i++) {
                        System.out.print(a[j][i] + "\t");
                    }
                    System.out.println(); // Move to the next line after each row
                }
                break;
            }
        }
    }
    