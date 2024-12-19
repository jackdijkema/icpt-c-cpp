#include <stdio.h>

typedef struct {
// column, row
  char board[7][6];
} Board;

int makeMove(_Bool turn);
void printBoard(Board board);
Board updateBoard(Board board, int move, _Bool turn);

int main() {
  int p1 = 0;
  int p2 = 0;
  _Bool turn = 0;
  int input;
  Board board;

  for (int i = 0; i < 7; i++) {
    for (int j = 0; j < 6; j++) {
      board.board[i][j] =  '0';
    }
  }

  do {
    turn = !turn;
    printBoard(board);
    int move = makeMove(turn);
    board = updateBoard(board, move, turn);

  } while (1);
}

void printBoard(Board board) {

  printf("0123456\n");
  printf("-----------------\n");

  for (int row = 0; row < 6; row++) {
        for (int column = 0; column < 7; column++) {
            printf("%c", board.board[column][row]);
          }    
        printf("%c", '\n');
   }
  printf("-----------------\n");
}

Board updateBoard(Board board, int move, _Bool turn) {
  for (int row = 5; row >= 0; row--) {
    if (board.board[move][row] ==  '0') {
      board.board[move][row] = turn ? 'X' : '+';
      break;
    }
  }
  return board;
}

int makeMove(_Bool turn) {
  int input;

  if (turn) {
    printf("Player 1 \n");
    printf("Make a move: ");
  }

  if (!turn) {
    printf("Player 2  \n");
    printf("Make a move: ");
  }

  scanf("%d", &input);

  if (input > 6) {
    printf("Column out of range needs to be =< 6\n");
    makeMove(turn);
    return -1;
  }

  return input;
};