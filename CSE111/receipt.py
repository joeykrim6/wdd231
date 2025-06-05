import csv

def read_dictionary(filename, key_column_index):
    """Read the contents of a CSV file into a compound
    dictionary and return the dictionary.
    Parameters
        filename: the name of the CSV file to read.
        key_column_index: the index of the column
            to use as the keys in the dictionary.
    Return: a compound dictionary that contains
        the contents of the CSV file.
    """
    result_dict = {}

    with open(filename, "r") as csvfile:
        reader = csv.reader(csvfile)
        next(reader)  # Skip header row

        for row in reader:
            if len(row) == 0:
                continue  # Skip empty rows
            key = row[key_column_index]
            # Convert price to float for accurate calculations later
            row[2] = float(row[2])
            result_dict[key] = row

    return result_dict


def main():
    # Read products.csv into a dictionary
    products_dict = read_dictionary("products.csv", 0)

    # Print the complete dictionary
    print("All Products")
    print(products_dict)

    # Print requested items
    print("Requested Items")
    with open("request.csv", "r") as request_file:
        reader = csv.reader(request_file)
        next(reader)  # Skip header row

        for row in reader:
            if len(row) < 2:
                continue  # Skip malformed lines
            product_number = row[0]
            quantity = int(row[1])

            if product_number in products_dict:
                product = products_dict[product_number]
                name = product[1]
                price = product[2]
                print(f"{name}: {quantity} @ {price:.2f}")
            else:
                print(f"Product {product_number} not found.")


# Protect the call to main
if __name__ == "__main__":
    main()
