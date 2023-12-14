namespace TennisKata;

public class Tennis1
{
    /*
    You task is to implement a tennis scoring program. Summary of tennis scoring:
    1. A game is won by the first player to have won at least four points in total and at least two points more than the opponent.
    2. The running score of each game is described in a manner peculiar to tennis: scores from zero to three points are described as "love", "fifteen", "thirty", and "forty" respectively.
    3. If at least three points have been scored by each player, and the scores are equal, the score is "deuce".
    4. If at least three points have been scored by each side and a player has one more point than his opponent, the score of the game is "advantage" for the player in the lead.
   */

    /* WARNING...
         Most examples I've seen of this directly couple the score with the output e.g. "Love-Love" is the starting gaem status
         Trying to calculate a score seperate to how this is displayed using TDD seems very hard.
         You can make the tests pass but 
     */
    
    [Fact]
    public void If_no_rallies_played_then_no_score()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");
        
        Assert.Equal("none", scorer1.CurrentScore());
    }

    [Fact]
    public void If_neither_player_has_scored_four_then_no_score()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");
        
        scorer1.Player1Win();
        scorer1.Player1Win(); // 30
        scorer1.Player2Win();
        scorer1.Player2Win(); // 30
        
        Assert.Equal("none", scorer1.CurrentScore());
    }

    [Fact]
    public void If_both_players_score_four_then_no_score()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");
        
        scorer1.Player1Win(); // 15-0
        scorer1.Player2Win(); // 15-15
        scorer1.Player1Win(); // 30-15
        scorer1.Player2Win(); // 30-30
        scorer1.Player1Win(); // 40-30
        scorer1.Player2Win(); // 40-40
        
        Assert.Equal("none", scorer1.CurrentScore());
    }
    
    [Fact]
    public void If_player1_scores_four_and_player2_scores_three_then_no_result()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");
        
        scorer1.Player1Win(); // 15-0
        scorer1.Player2Win(); // 15-15
        scorer1.Player1Win(); // 30-15
        scorer1.Player2Win(); // 30-30
        scorer1.Player1Win(); // 40-30
        scorer1.Player2Win(); // 40-40
        scorer1.Player1Win(); // ADV
        
        Assert.Equal("none", scorer1.CurrentScore());
    }
    
    [Fact]
    public void If_player2_scores_four_and_player2_scores_three_then_no_result()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");
        
        scorer1.Player1Win(); // 15-0
        scorer1.Player2Win(); // 15-15
        scorer1.Player1Win(); // 30-15
        scorer1.Player2Win(); // 30-30
        scorer1.Player1Win(); // 40-30
        scorer1.Player2Win(); // 40-40
        scorer1.Player2Win(); // ADV
        
        Assert.Equal("none", scorer1.CurrentScore());
    }
    
    [Fact]
    public void Player1_wins_by_advantage()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");
        
        scorer1.Player1Win(); // 15-0
        scorer1.Player2Win(); // 15-15
        scorer1.Player1Win(); // 30-15
        scorer1.Player2Win(); // 30-30
        scorer1.Player1Win(); // 40-30
        scorer1.Player2Win(); // 40-40
        scorer1.Player1Win(); // ADV
        scorer1.Player1Win(); // WIN
        
        Assert.Equal("Bill", scorer1.CurrentScore());
    }
    
    [Fact]
    public void Player2_wins_by_advantage()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");
        
        scorer1.Player1Win(); // 15-0
        scorer1.Player2Win(); // 15-15
        scorer1.Player1Win(); // 30-15
        scorer1.Player2Win(); // 30-30
        scorer1.Player1Win(); // 40-30
        scorer1.Player2Win(); // 40-40
        scorer1.Player2Win(); // ADV
        scorer1.Player2Win(); // WIN
        
        Assert.Equal("Jane", scorer1.CurrentScore());
    }

    [Fact]
    public void Player1_wins_by_advantage_multiple_deuces()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");

        scorer1.Player1Win(); // 15-0
        scorer1.Player2Win(); // 15-15
        scorer1.Player1Win(); // 30-15
        scorer1.Player2Win(); // 30-30
        scorer1.Player1Win(); // 40-30
        scorer1.Player2Win(); // 40-40
        scorer1.Player1Win(); // ADV Player 1
        scorer1.Player2Win(); // deuce
        scorer1.Player2Win(); // ADV Player 2
        scorer1.Player1Win(); // deuce
        scorer1.Player1Win(); // ADV Player 1
        scorer1.Player1Win(); // WIN

        Assert.Equal("Bill", scorer1.CurrentScore());
    }

    [Fact]
    public void Player2_wins_by_advantage_multiple_deuces()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");

        scorer1.Player1Win(); // 15-0
        scorer1.Player2Win(); // 15-15
        scorer1.Player1Win(); // 30-15
        scorer1.Player2Win(); // 30-30
        scorer1.Player1Win(); // 40-30
        scorer1.Player2Win(); // 40-40
        scorer1.Player1Win(); // ADV Player 1
        scorer1.Player2Win(); // deuce 
        scorer1.Player2Win(); // ADV Player 2
        scorer1.Player1Win(); // deuce
        scorer1.Player2Win(); // ADV Player 2
        scorer1.Player2Win(); // WIN

        Assert.Equal("Jane", scorer1.CurrentScore());
    }
    
    [Fact]
    public void player2_advantage_multiple_deuces()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");

        scorer1.Player1Win(); // 15-0
        scorer1.Player2Win(); // 15-15
        scorer1.Player1Win(); // 30-15
        scorer1.Player2Win(); // 30-30
        scorer1.Player1Win(); // 40-30
        scorer1.Player2Win(); // 40-40
        scorer1.Player1Win(); // ADV Player 1
        scorer1.Player2Win(); // deuce 
        scorer1.Player2Win(); // ADV Player 2
        scorer1.Player1Win(); // deuce
        scorer1.Player2Win(); // ADV Player 2
        //scorer1.Player2Win(); // WIN

        Assert.Equal("none", scorer1.CurrentScore());
    }
    
   

    [Fact]
    public void Player1_wins_all_points()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");

        scorer1.Player1Win();
        scorer1.Player1Win();
        scorer1.Player1Win();
        scorer1.Player1Win();
        
        Assert.Equal("Bill", scorer1.CurrentScore());
    }
    
    [Fact]
    public void Player2_wins_all_points()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");

        scorer1.Player2Win();
        scorer1.Player2Win();
        scorer1.Player2Win();
        scorer1.Player2Win();

        Assert.Equal("Jane", scorer1.CurrentScore());
    }

    [Fact]
    public void Player1_wins_more_then_player2()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");

        scorer1.Player2Win();
        scorer1.Player1Win();
        scorer1.Player1Win();
        scorer1.Player1Win();
        scorer1.Player1Win();

        Assert.Equal("Bill", scorer1.CurrentScore()); 
    }
    
    [Fact]
    public void Player2_wins_more_then_player1()
    {
        TennisScorer1 scorer1 = new TennisScorer1("Bill", "Jane");

        scorer1.Player1Win();
        scorer1.Player2Win();
        scorer1.Player2Win();
        scorer1.Player2Win();
        scorer1.Player2Win();

        Assert.Equal("Jane", scorer1.CurrentScore()); 
    }
}

public class TennisScorer1
{
    private readonly string _player1Name;
    private readonly string _player2Name;

    private int _player1WinCount = 0;
    private int _player2WinCount = 0;
    
    private string _winner;

    public TennisScorer1(string player1Name, string player2Name)
    {
        _player1Name = player1Name;
        _player2Name = player2Name;
    }

    public void Player1Win()
    {
        _player1WinCount++;
    }
    
    public void Player2Win()
    {
        _player2WinCount++;
    }

    public string CurrentScore()
    {
        if (_player1WinCount < 4 && _player2WinCount < 4)
            return "none";

        if (_player1WinCount == 4 && _player2WinCount == 3)
            return "none";
        
        if (_player2WinCount == 4 && _player1WinCount == 3)
            return "none";

        if (_player1WinCount >= 4 && _player1WinCount > _player2WinCount + 1)
            return _player1Name;
        
        if (_player2WinCount >= 4 && _player2WinCount > _player1WinCount + 1)
            return _player2Name;
        
        return _player1WinCount > _player2WinCount ? _player1Name : _player2Name;
    }
}