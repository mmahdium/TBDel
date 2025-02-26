# TBDel (To Be Deleted)
### A simple command-line tool for managing a list of files and directories marked for deletion. User can add, remove, list, and execute the deletion of these entries.

## Installation

- Install on Arch Linux (aur)
```shell
paru -S tbdel
```

## Usage

```shell
Usage: tbdel <command> [arguments]

Available commands:
add <path to file or folder>       Add a file or folder to the list
delete <file or folder ID>         Deletes a file or folder
rmflist          Remove an entry ONLY from the list
deleteall        Deletes all items in the list
list             Lists all items in the list
help             Shows this help message
```
## Examples

* Add a file:
  ```shell
  tbdel add /path/to/my/file.txt
  ```

* Add a folder:
  ```shell
  tbdel add /path/to/my/folder
  ```

* List all entries:
  ```shell
  tbdel list
  ```
  
* Remove an entry ONLY from the list: **(Assuming '12345' is the ID of the entry you want to remove from the list)**
  ```shell
  tbdel rmflist 12345
  ```

* Delete an entry (using its ID): **(Assuming '12345' is the ID of the entry you want to delete)**
  ```shell
  tbdel delete 12345
  ```

* Delete all entries:
  ```shell
  tbdel deleteall
  ```

* Get help:
  ```shell
  tbdel help
  ```

## Contributing

#### All contributions are welcome!  Feel free to submit pull requests, bug reports, or feature requests.

## License

This project is licensed under the GNU Affero General Public License v3.0 - see the [LICENSE](LICENSE) file for details.
